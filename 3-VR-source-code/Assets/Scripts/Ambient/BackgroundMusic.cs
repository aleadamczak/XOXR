using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public AudioClip[] bgms;

    public AudioSource audioSource;
    private int currentIndex = 0;

    void Start()
    {
        if (bgms.Length > 0 && audioSource != null)
        {
            PlayCurrentClip();
        }
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextClip();
        }
    }

    private void PlayCurrentClip()
    {
        audioSource.clip = bgms[currentIndex];
        audioSource.Play();
    }

    private void PlayNextClip()
    {
        currentIndex = (currentIndex + 1) % bgms.Length;
        PlayCurrentClip();
    }
}
