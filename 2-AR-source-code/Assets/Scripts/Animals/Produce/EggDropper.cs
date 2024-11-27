using UnityEngine;
using System.Collections;

public class EggDropper : MonoBehaviour
{
    private AnimalSoundController soundController;
    public GameObject eggPrefab;
    public float dropDistance = 1f; // distance behind the chicken to drop the egg
    public float dropInterval = 60f;
    private Produce chickenProduce;

    void Start()
    {
        chickenProduce = GetComponent<Produce>();
        soundController = GetComponent<AnimalSoundController>();
        StartCoroutine(DropEggs());
    }

    private IEnumerator DropEggs()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            DropEgg();
            yield return new WaitForSeconds(dropInterval);
        }
    }

    private void DropEgg()
    {
        Vector3 dropPosition = transform.position - transform.forward * dropDistance + Vector3.down * 0.2f;
        GameObject egg = Instantiate(eggPrefab, dropPosition, Quaternion.identity);
        EggPickup eggPickup = egg.GetComponent<EggPickup>();
        if (eggPickup != null)
        {
            eggPickup.SetQuality(chickenProduce.GetProduceQuality());
        }
        soundController.EggDrop();
    }
}