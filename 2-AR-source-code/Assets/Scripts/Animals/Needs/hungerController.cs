
using UnityEngine;
using System;

public class hungerController : MonoBehaviour
{

    public int hunger = 100;
    public float hungerRate = 5;
    [SerializeField] hungerBar hungerBar;
    public int thirst = 100;
    public float thirstRate = 5;
    [SerializeField] thirstBar thirstBar;
    private AnimalSoundController soundController;
    private DeathManager deathManager;
    private bool died;

    void Awake()
    {
      hungerBar = GetComponentInChildren<hungerBar>();
      thirstBar = GetComponentInChildren<thirstBar>();
      soundController = GetComponent<AnimalSoundController>();
      deathManager = GetComponent<DeathManager>();
    }
    
    void Start()
    {
        InvokeRepeating("IncreaseHunger", 0, hungerRate);
        InvokeRepeating("IncreaseThirst", 0, thirstRate);
    }

    void Update()
    {
        if (!died)
        {
            CheckForDeath();
        }
    }

    void CheckForDeath()
    {
        if(hunger==0 || thirst==0)
        {
            deathManager.Die();
            died = true;
        }
    }

    public void setHunger(int value)
    {
        hunger = value;
        hungerBar.updateHungerBar(hunger);
    }

    public void setThirst(int value)
    {
        thirst = value;
        thirstBar.updateThirstBar(thirst);
    }

    public void IncreaseHunger()
    {
        if (hunger > 0) {hunger--;
            hungerBar.updateHungerBar(hunger);
        }
    }

    public void IncreaseThirst()
    {
        if (thirst > 0)
        {
            thirst--;
            thirstBar.updateThirstBar(thirst);
        }
    }


    public void Eat()
    {
        if (hunger < 100)
        {
            hunger += 20;
            if (hunger>100)
            {
                hunger = 100;
            }
        }
        hungerBar.updateHungerBar(hunger);
        soundController.Eat();
    }

    public void Drink()
    {
        if (thirst < 100)
        {
            thirst += 20;
            if (thirst > 100)
            {
                thirst = 100;
            }
        }
        thirstBar.updateThirstBar(thirst);
        soundController.Drink();
    }
}
