using UnityEngine;
using UnityEngine.Events;

public class DeliveryStationHandler : MonoBehaviour
{
    public UnityEvent<GameObject> onPizzaDelivered;
    public AudioSource audioSource;
    public AudioClip upsetClip;
    public AudioClip decentClip;
    public AudioClip happyClip;
    public AudioClip overjoyedClip;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pizza"))
        {
            onPizzaDelivered?.Invoke(other.gameObject);
        }
    }
    
    public void PlayReactionAudio(int finalScore)
    {
        int upsetThreshold = 100 + 60 + 80 + 40; // example "low effort" threshold (280 in this case)
        int maxScore = 400;

        if (finalScore < upsetThreshold)
        {
            PlayClip(upsetClip);
        }
        else
        {
            int range = (maxScore - upsetThreshold) / 3;
            if (finalScore < upsetThreshold + range)
            {
                PlayClip(decentClip);
            }
            else if (finalScore < upsetThreshold + 2 * range)
            {
                PlayClip(happyClip);
            }
            else
            {
                PlayClip(overjoyedClip);
            }
        }
    }

    private void PlayClip(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing!");
        }
    }
}
