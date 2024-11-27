using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonsStaySelected : MonoBehaviour
{
    private Button lastSelectedButton;
    void Update()
    {
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
        if (selectedObject != null && selectedObject.GetComponent<Button>() != null)
        {
            lastSelectedButton = selectedObject.GetComponent<Button>();
        }
        else if (lastSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelectedButton.gameObject);
        }
    }
}
