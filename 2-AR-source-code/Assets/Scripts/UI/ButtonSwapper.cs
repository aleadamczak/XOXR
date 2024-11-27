using UnityEngine;
using UnityEngine.UI;

public class ButtonSwapper : MonoBehaviour
{
    private int activeButtonIndex;
    private int buttonsCount;
    public Button[] actionButtons;
    private Button activeButton;

    private Vector2 startSwipePosition;
    private bool isSwiping;

    void Start()
    {
        if (actionButtons == null || actionButtons.Length == 0)
        {
            return;
        }
        buttonsCount = actionButtons.Length;
        UpdateActiveButton();
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            ProcessSwipe(touch.position, touch.phase == TouchPhase.Began, touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled);
        }
        else if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)) // Mouse input
        {
            bool isStarting = Input.GetMouseButtonDown(0);
            bool isEnding = Input.GetMouseButtonUp(0);
            ProcessSwipe(Input.mousePosition, isStarting, isEnding);
        }
    }

    void ProcessSwipe(Vector2 position, bool isStarting, bool isEnding)
    {
        if (isStarting && IsPointerOverButton(activeButton, position))
        {
            startSwipePosition = position;
            isSwiping = true;
        }
        else if (isSwiping && !isEnding)
        {
            Vector2 swipeDelta = position - startSwipePosition;

            if (Mathf.Abs(swipeDelta.y) > Mathf.Abs(swipeDelta.x)) 
            {
                if (swipeDelta.y > 0)
                    NextButton();
                else
                    PreviousButton();

                isSwiping = false;
            }
        }
        else if (isEnding)
        {
            isSwiping = false;
        }
    }

    void UpdateActiveButton()
    {
        activeButton = actionButtons[activeButtonIndex];
        for (int i = 0; i < actionButtons.Length; i++)
        {
            actionButtons[i].gameObject.SetActive(i == activeButtonIndex);
        }
    }

    void NextButton()
    {
        activeButtonIndex = (activeButtonIndex + 1) % buttonsCount;
        UpdateActiveButton();
    }

    void PreviousButton()
    {
        activeButtonIndex = (activeButtonIndex - 1 + buttonsCount) % buttonsCount;
        UpdateActiveButton();
    }

    bool IsPointerOverButton(Button button, Vector2 pointerPosition)
    {
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, pointerPosition, null);
    }
}
