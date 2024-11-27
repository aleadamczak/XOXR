using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class IngredientGrabber : XRGrabInteractable
{
    public GameObject ingredientPrefab;
    public XRBaseInteractor interactor;
    public void grabIngredient(SelectEnterEventArgs args)
    {
        interactionManager.CancelInteractorSelection(args.interactorObject);
        GameObject go= Instantiate( ingredientPrefab );
        XRGrabInteractable objectInteractable = go.GetComponent<XRGrabInteractable>();
        interactionManager.SelectEnter(args.interactorObject, objectInteractable);
    }
}
