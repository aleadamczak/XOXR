using Animals.Produce;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class GUIManager : MonoBehaviour
{
    public Button swordButton;
    public Button bucketButton;
    public Button shearsButton;
    public Button wheatButton;
    public Button waterButton;
    public Button carrotButton;
    public Button seedsButton;
    public Button restartButton;
    public Button startGameButton;
    public ARSession ArSession;
    public GameObject instructionsCanvas;
    public GameObject mainCanvas;
    public SceneInitializer sceneInitializer;

    public Items selectedItem = Items.Nothing;
    private Button selectedButton;

    private bool isDeselecting;

    void Start()
    {
        swordButton.onClick.AddListener(() => ItemOnClick(swordButton, Items.Sword));
        bucketButton.onClick.AddListener(() => ItemOnClick(bucketButton, Items.Bucket));
        shearsButton.onClick.AddListener(() => ItemOnClick(shearsButton, Items.Shears));
        wheatButton.onClick.AddListener(() => ItemOnClick(wheatButton, Items.Wheat));
        waterButton.onClick.AddListener(() => ItemOnClick(waterButton, Items.Water));
        carrotButton.onClick.AddListener(() => ItemOnClick(carrotButton, Items.Carrot));
        seedsButton.onClick.AddListener(() => ItemOnClick(seedsButton, Items.Seeds));
        restartButton.onClick.AddListener(() => RestartScene());
        startGameButton.onClick.AddListener(() => StartGame());
    }

    void Update()
    {
        if (selectedItem != Items.Nothing && !isDeselecting && EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
        }
    }


    public void ItemOnClick(Button button, Items item)
    {
        if (selectedItem == item)
        {
            selectedButton = null;
            isDeselecting = true;
            EventSystem.current.SetSelectedGameObject(null);
            selectedItem = Items.Nothing;
        }
        else
        {
            selectedButton = button;
            isDeselecting = false;
            EventSystem.current.SetSelectedGameObject(button.gameObject);
            selectedItem = item;
        }
    }

    public void RestartScene()
    {
        AnimalCounter.firstAnimalPlaced = false;
        ArSession.Reset(); // this is a reference to an ARSession object from the scene
        LoaderUtility.Deinitialize();
        LoaderUtility.Initialize();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        instructionsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
        sceneInitializer.enabled= true;
    }
}