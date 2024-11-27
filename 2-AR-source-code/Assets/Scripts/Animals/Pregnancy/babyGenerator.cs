using System;
using UnityEngine;
using UnityEngine.UIElements;

public class babyGenerator : MonoBehaviour
{
    public int oneInXPossibility = 5;
    public string animalBreed;
    public GameObject baby;
    [SerializeField] hungerController hungerController;
    [SerializeField] pregnancyController pregnancyController;
    private Vector3 positionForBaby;
    private Quaternion rotationForBaby;
    public int pregnancyDuration=100;
    public bool pregnant=false;
    public ParticleSystem pregnancyHearts;

    void Awake()
    {
        hungerController = GetComponent<hungerController>();
        pregnancyController= GetComponent<pregnancyController>();
    }

    void Update()
    {
        if(pregnancyController.pregnancy==0)
        {
            pregnant = false;
            pregnancyController.babyBar.gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    { if (!pregnant) { 
        if (AnimalCounter.animalCount < AnimalCounter.maxAnimalCount)
        {
            if (hungerController.hunger > 50 && hungerController.thirst > 50)
            {
                if (other.gameObject.CompareTag(animalBreed))
                {
                    hungerController otherHungerController = other.gameObject.GetComponent<hungerController>();
                    babyGenerator otherBabyGenerator = other.gameObject.GetComponent<babyGenerator>();
                    if (otherBabyGenerator != null && otherHungerController != null &&
                        otherHungerController.hunger > 50 &&
                        otherHungerController.thirst > 50)
                    {
                        if (UnityEngine.Random.Range(0, oneInXPossibility) == 1)
                        {
                            pregnant= true;
                            other.gameObject.GetComponent<babyGenerator>().pregnant = true;
                            other.gameObject.GetComponent<pregnancyController>().startPregnancy(pregnancyDuration);
                            pregnancyController.startPregnancy(pregnancyDuration);
                            Invoke("haveBaby", pregnancyDuration);
                            Transform otherTransform = other.transform;
                            positionForBaby = otherTransform.position;
                            rotationForBaby = otherTransform.rotation;
                            Quaternion heartsRotation = Quaternion.Euler(-90f, 0f, 0f);
                            ParticleSystem heartsThis = Instantiate(pregnancyHearts, transform.position, heartsRotation, transform);
                            heartsThis.Play();
                            ParticleSystem heartsOther = Instantiate(pregnancyHearts, positionForBaby, heartsRotation, otherTransform);
                            heartsOther.Play();
                            Destroy(heartsThis.gameObject, 10);
                            Destroy(heartsOther.gameObject, 10);
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("Too many animals in the scene, did not generate more babies...");
        }}
    }

    private void haveBaby()
    {
        AnimalCounter.animalCount++;
        Instantiate(baby, positionForBaby, rotationForBaby);
    }
}

