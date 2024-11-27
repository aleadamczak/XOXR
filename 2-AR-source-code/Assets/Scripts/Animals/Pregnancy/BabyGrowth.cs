using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyGrowth : MonoBehaviour
{
    public GameObject parentVersion;
    public float growthDuration = 60f;
    private hungerController babyHungerController;

    void Start()
    {
        babyHungerController = GetComponent<hungerController>();
        StartCoroutine(GrowUp());
    }

    private IEnumerator GrowUp() {
        yield return new WaitForSeconds(growthDuration);
        GameObject parentGo = Instantiate(parentVersion, transform.position, transform.rotation);
        hungerController parentHunger = parentGo.GetComponent<hungerController>();
        parentHunger.setHunger(babyHungerController.hunger);
        parentHunger.setThirst(babyHungerController.thirst);
        Destroy(gameObject);
    }
}
