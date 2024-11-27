using System;
using UnityEngine;

public class DoughController : MonoBehaviour
{
    private int doughCount;
    private Boolean firstTime = true;
    public GameObject doughs;

    void Start()
    {
        doughCount = 0;
    }

    void Update()
    {
        if (!firstTime && doughCount == 0)
        {
            Instantiate(doughs);
            firstTime = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Dough")
        {
            doughCount--;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Dough")
        {
            firstTime = false;
            doughCount++;
        }
    }
}