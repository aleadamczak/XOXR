using UnityEngine;
using UnityEngine.UI;
using System;


public class thirstBar : MonoBehaviour
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

    public void updateThirstBar(float thirst)
    {
        slider.value = thirst / 100;
    }
}
