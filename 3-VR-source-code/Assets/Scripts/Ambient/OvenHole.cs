using System;
using DefaultNamespace;
using UnityEngine;

public class OvenHole : MonoBehaviour
{
    public Clock clock;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pizza"))
        {
            Pizza pizza = other.gameObject.GetComponent<Pizza>();
            PizzaObject innerPizza = pizza.pizza;
            clock.SetTime(innerPizza.GetTimeCooked());
            clock.StartTicking();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Pizza"))
        {
            Pizza pizza = other.gameObject.GetComponent<Pizza>();
            PizzaObject innerPizza = pizza.pizza;
            pizza.SetCookedTime(clock.timePassed);
            clock.StopTicking();
            clock.Reset();
        }
    }
}
