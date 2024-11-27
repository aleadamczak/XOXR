using System;
using DefaultNamespace;
using UnityEngine;

public class PizzaQuarter : MonoBehaviour
{
    public int quarterNumber;
    private Pizza pizza;
    void Start()
    {
        pizza= GetComponentInParent<Pizza>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Enum.TryParse(other.gameObject.tag, out Ingredient ingredient))
        {
            pizza.AddIngredient(ingredient, quarterNumber);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Enum.TryParse(other.gameObject.tag, out Ingredient ingredient))
        {
            pizza.RemoveIngredient(ingredient, quarterNumber);
        }
    }
}
