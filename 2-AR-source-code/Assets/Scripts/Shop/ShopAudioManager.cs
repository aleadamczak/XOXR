using UnityEngine;

public class ShopAudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayAudio()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
    }
}
