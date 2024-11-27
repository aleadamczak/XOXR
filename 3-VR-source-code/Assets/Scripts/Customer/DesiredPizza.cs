using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class DesiredPizza : MonoBehaviour
{
    public int timeToCook;
    public List<Ingredient> ingredientsQuarter1;
    public List<Ingredient> ingredientsQuarter2;
    public List<Ingredient> ingredientsQuarter3;
    public List<Ingredient> ingredientsQuarter4;
    public bool verticalCut;
    public bool horizontalCut;
    public bool diagonalCut1;
    public bool diagonalCut2;

    public PizzaObject DesiredPizzaObject()
    {
        return new PizzaObject(
            timeToCook,
            ingredientsQuarter1,
            ingredientsQuarter2,
            ingredientsQuarter3,
            ingredientsQuarter4,
            verticalCut,
            horizontalCut,
            diagonalCut1,
            diagonalCut2,
            true,
            true
        );
    }
}