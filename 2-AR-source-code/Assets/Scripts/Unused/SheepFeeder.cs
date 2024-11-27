using System.Collections.Generic;
using Animals.Produce;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using static UnityEngine.EventSystems.PointerEventData;

public class SheepFeeder : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public Camera arCamera;
    private hungerController sheep;
    private GUIManager guiManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        guiManager = GameObject.Find("GUI Manager").GetComponent<GUIManager>();
        if (guiManager == null)
        {
            throw new System.Exception("GUIManager not found in the scene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        List<ARRaycastHit> hits = new();

        // Detect touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            FeedSheep(hits, Input.GetTouch(0).position);
        }
        // Detect mouse input
        else if (Input.GetMouseButtonDown(0))
        {
            FeedSheep(hits, Input.mousePosition);
        }
    }

    void InteractWithAnimal(List<ARRaycastHit> hits, Vector3 position)
    {
        Ray ray = arCamera.ScreenPointToRay(position);
        RaycastHit hit;
      
        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.collider.gameObject;

            if (objectHit.CompareTag("Animal"))
            {
                sheep = objectHit.GetComponent<hungerController>();
                sheep.Eat();
            }
        }
    }

    void FeedSheep(List<ARRaycastHit> hits, Vector3 position)
    {
        Ray ray = arCamera.ScreenPointToRay(position);
        RaycastHit hit;
      
        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.collider.gameObject;

            if (objectHit.CompareTag("sheep"))
            {
                sheep = objectHit.GetComponent<hungerController>();
                sheep.Eat();
            }
        }
    }
}
