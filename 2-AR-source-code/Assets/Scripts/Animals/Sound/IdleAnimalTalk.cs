using System.Collections;
using UnityEngine;

public class IdleAnimalTalk : MonoBehaviour
{
    public int minSoundCooldown = 5;
    public int maxSoundCooldown = 10;
    private AnimalSoundController soundController;
    void Start()
    {
        soundController = GetComponent<AnimalSoundController>();
        StartCoroutine(IdleSoundRoutine());

    }
    
    IEnumerator IdleSoundRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minSoundCooldown, maxSoundCooldown);
            yield return new WaitForSeconds(waitTime);
            soundController.Idle();
        }
    }
}
