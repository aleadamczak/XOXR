
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.InputSystem.EnhancedTouch;

public class pregnancyController : MonoBehaviour
{
    public int pregnancyMax;
    public int pregnancy;
    public babyBar babyBar;
    private AnimalSoundController soundController;

    void Start()
    {
        soundController = GetComponent<AnimalSoundController>();
    }
    void Awake()
    {
        babyBar = GetComponentInChildren<babyBar>();
        babyBar.gameObject.SetActive(false);
    }
   
    public void startPregnancy(int time)
    {
        babyBar.gameObject.SetActive(true);
        pregnancy = time;
        pregnancyMax = time;
        soundController.Pregnant();
        InvokeRepeating("IncreasePregnancy", 0, 1);
    }

    public void IncreasePregnancy()
    {
        if (pregnancy > 0)
        {
            pregnancy--;
            babyBar.updateBabyBar(pregnancy, pregnancyMax);
        }
    }

}
