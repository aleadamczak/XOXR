using UnityEngine;
using UnityEngine.UI;
using System;


public class hungerBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void updateHungerBar(float hunger)
    {
        slider.value = hunger / 100;
    }
}
