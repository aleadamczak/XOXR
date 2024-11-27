using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TouchPhase = UnityEngine.TouchPhase;

public class SceneInitializer : MonoBehaviour
{
    public GameObject sheepPrefab;
    public GameObject babySheepPrefab;
    public GameObject cowPrefab;
    public GameObject pigPrefab;
    public GameObject chickenPrefab;
    public ARRaycastManager raycastManager;
    public ShopManager shopManager;
    public CanvasSwitcher canvasManager;
    private int counter;
    private int animalsToBePlaced;
    private List<ARRaycastHit> hits;
    private bool placingMode;

    void Start()
    {
        counter = 0;
        animalsToBePlaced = 0;
        hits = new List<ARRaycastHit>();
    }

    public void EnterPlacingMode()
    {
        placingMode = true;
    }

    public void LeavePlacingMode()
    {
        placingMode = false;
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (counter < 2)
            {
                PlaceObject(hits, Input.GetTouch(0).position, sheepPrefab);
            }
            else if (animalsToBePlaced > 0)
            {
                Dictionary<string, int> basket = shopManager.GetBasketItems();
                while (animalsToBePlaced > 0)
                {
                    PlaceBasketItem(ReturnFirstItem(basket), Input.mousePosition);
                }
                shopManager.ClearBasket();
                canvasManager.ShowMainCanvas();
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (counter < 2)
            {
                PlaceObject(hits, Input.mousePosition, sheepPrefab, true);
            }
            else if (animalsToBePlaced > 0 && placingMode)
            {
                Dictionary<string, int> basket = shopManager.GetBasketItems();
                while (animalsToBePlaced > 0)
                {
                    PlaceBasketItem(ReturnFirstItem(basket), Input.mousePosition);
                }
                if (animalsToBePlaced == 0)
                {
                    shopManager.ClearBasket();
                    canvasManager.ShowMainCanvas();
                    LeavePlacingMode();
                }
            }
        }
    }


    void PlaceObject(List<ARRaycastHit> hits, Vector3 position, GameObject prefab, bool firstAnimals = false)
    {
        if (raycastManager.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
        {
            if (!AnimalCounter.firstAnimalPlaced)
            {
                AnimalCounter.firstAnimalPlaced = true;
            }
            AnimalCounter.animalCount++;
            Pose hitPose = hits[0].pose;
            Vector3 targetPosition = hitPose.position + (Vector3.up) * 0.5f;
            Instantiate(prefab, targetPosition, hitPose.rotation);
            counter++;
            if (!firstAnimals) animalsToBePlaced--;
        }
    }

    public void setAnimalsToBePlaced(int quantity)
    {
        animalsToBePlaced = quantity;
    }

    public void PlaceBasketItem(KeyValuePair<string, int> animal, Vector3 position)
    {
        for (int i = animal.Value; i > 0; i--)
        {
            switch (animal.Key)
            {
                case "Sheep":
                    PlaceObject(hits, position, babySheepPrefab);
                    break;
                case "Cow":
                    PlaceObject(hits, position, cowPrefab);
                    break;
                case "Pig":
                    PlaceObject(hits, position, pigPrefab);
                    break;
                case "Chicken":
                    PlaceObject(hits, position, chickenPrefab);
                    break;
            }
        }
    }

    public static KeyValuePair<string, int> ReturnFirstItem(Dictionary<string, int> dict)
    {
        var firstItem = dict.First();
        dict.Remove(firstItem.Key);
        return firstItem;
    }
}