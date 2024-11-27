using System;
using UnityEngine;

public class SauceSpoonControler : MonoBehaviour
{
    public GameObject sauce;

    void Start()
    {
        sauce.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("TomatoBowl"))
        {
            sauce.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Pizza") && sauce.activeInHierarchy)
        {
            sauce.SetActive(false);
            other.collider.GetComponent<Pizza>().AddSauce();
        }
    }
}
