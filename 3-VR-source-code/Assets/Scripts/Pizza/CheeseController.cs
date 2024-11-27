using System;
using UnityEngine;
using static UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation.XRDeviceSimulator;

public class CheeseController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Pizza"))
        {
            other.collider.GetComponent<Pizza>().AddCheese();
            Destroy(gameObject);
        }
    }
}