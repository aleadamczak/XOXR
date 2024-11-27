using UnityEngine;
using System;

public class Cut : MonoBehaviour
{
    public string cutId;

    public event Action<string> OnCutTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PizzaCutter"))
        {

            OnCutTriggered?.Invoke(cutId);
        }
        
    }
}