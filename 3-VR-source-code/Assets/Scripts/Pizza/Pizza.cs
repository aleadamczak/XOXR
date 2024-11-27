using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pizza : MonoBehaviour
{
    public PizzaObject pizza;
    public AudioSource audio;
    public Material baseMaterial;
    public Material sauceMaterial;
    public Material cheeseMaterial;
    public Material onlyCheeseMaterial;
    public Rigidbody rb;
    public GameObject[] slices;
    private string activematerial = "base";
    


    void Start()
    {
        pizza = PizzaObject.EmptyPizza();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pepperoni") || collision.gameObject.CompareTag("Olive") ||
            collision.gameObject.CompareTag("Pepper") || collision.gameObject.CompareTag("Fish") ||
            collision.gameObject.CompareTag("Mushroom"))
        {
            collision.gameObject.transform.parent = transform;
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }


    public void SetCookedTime(float secondsInOven)
    {
        pizza.SetTimeCooked(secondsInOven);
    }

    public void AddIngredient(Ingredient ingredient, int quarter)
    {
        switch (quarter)
        {
            case 1:
                pizza.ingredientsQuarter1.Add(ingredient);
                break;
            case 2:
                pizza.ingredientsQuarter2.Add(ingredient);
                break;
            case 3:
                pizza.ingredientsQuarter3.Add(ingredient);
                break;
            case 4:
                pizza.ingredientsQuarter4.Add(ingredient);
                break;
        }
    }

    public void RemoveIngredient(Ingredient ingredient, int quarter)
    {
        switch (quarter)
        {
            case 1:
                pizza.ingredientsQuarter1.Remove(ingredient);
                break;
            case 2:
                pizza.ingredientsQuarter2.Remove(ingredient);
                break;
            case 3:
                pizza.ingredientsQuarter3.Remove(ingredient);
                break;
            case 4:
                pizza.ingredientsQuarter4.Remove(ingredient);
                break;
        }
    }

    public void AddCheese()
    {
        foreach (GameObject slice in slices)
        {
            if (activematerial == "sauce")
            {
                slice.gameObject.GetComponent<Renderer>().material = cheeseMaterial;
            }
            else if (activematerial == "base")
            {
                slice.gameObject.GetComponent<Renderer>().material = onlyCheeseMaterial;
            }
        }

        pizza.cheese = true;
        activematerial = "cheese";
    }

    public void AddSauce()
    {
        if (activematerial == "cheese")
        {
            pizza.cheese = false;
        }

        foreach (GameObject slice in slices)
        {
            slice.gameObject.GetComponent<Renderer>().material = sauceMaterial;
            activematerial = "sauce";
        }

        pizza.sauce = true;
    }

    public void AddPizzaCut(string typeOfCut)
    {
        switch (typeOfCut)
        {
            case "vertical":
                pizza.verticalCut = true;
                break;
            case "horizontal":
                pizza.horizontalCut = true;
                break;
            case "diagonal1":
                pizza.diagonalCut1 = true;
                break;
            case "diagonal2":
                pizza.diagonalCut2 = true;
                break;
            
        }
        audio.Play();
    }
}