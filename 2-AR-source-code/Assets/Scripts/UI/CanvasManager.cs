using UnityEngine;

public class CanvasSwitcher : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas shopCanvas;
    public Canvas newAnimalsCanvas;
    public ProducePopUp producePopUp;


    public void ShowMainCanvas()
    {
        mainCanvas.gameObject.SetActive(true);
        shopCanvas.gameObject.SetActive(false);
        newAnimalsCanvas.gameObject.SetActive(false);
    }

    public void ShowShopCanvas()
    {
        producePopUp.Interrupt();
        mainCanvas.gameObject.SetActive(false);
        shopCanvas.gameObject.SetActive(true);
        newAnimalsCanvas.gameObject.SetActive(false);
    }

    public void ShowNewAnimalsCanvas()
    {
        mainCanvas.gameObject.SetActive(false);
        shopCanvas.gameObject.SetActive(false);
        newAnimalsCanvas.gameObject.SetActive(true);

    }
}
