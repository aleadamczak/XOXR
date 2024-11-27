using UnityEngine;

public class PizzaCutter : MonoBehaviour
{
    public string typeOfCut;
    public Pizza pizza;
    public Cut edgeCut1;
    public Cut edgeCut2;
    public Cut centerCut;
    public Vector3 cutDirection = new Vector3(0, 0, 0);
    public GameObject[] pizzaSlices;
    private enum CollisionState
    {
        None,
        FirstEdge,
        Center,
        SecondEdge
    }

    private CollisionState currentState;

    private void Start()
    {
        
        edgeCut1.OnCutTriggered += HandleEdgeCutTriggered;
        edgeCut2.OnCutTriggered += HandleEdgeCutTriggered;
        centerCut.OnCutTriggered += HandleCenterCutTriggered;
        currentState = CollisionState.None;
    }

    private void OnDestroy()
    {
        edgeCut1.OnCutTriggered -= HandleEdgeCutTriggered;
        edgeCut2.OnCutTriggered -= HandleEdgeCutTriggered;
        centerCut.OnCutTriggered -= HandleCenterCutTriggered;
    }

    private void HandleEdgeCutTriggered(string cutId)
    {
        switch (currentState)
        {
            case CollisionState.None:
                currentState = CollisionState.FirstEdge;
                break;
            case CollisionState.Center:
                currentState = CollisionState.SecondEdge;
                ApplyCut();
                break;
        }
    }
    
    private void HandleCenterCutTriggered(string cutId)
    {
        if (currentState != CollisionState.FirstEdge) return;
        currentState = CollisionState.Center;
    }

    private void ApplyCut()
    {
        foreach (GameObject slice in pizzaSlices)
        {
            slice.transform.position += cutDirection;
        }
        pizza.AddPizzaCut(typeOfCut);
    }
}
