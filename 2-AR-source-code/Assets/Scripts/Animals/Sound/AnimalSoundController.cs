using UnityEngine;
using UnityEngine.Serialization;

public class AnimalSoundController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] idleClips;
    public AudioClip[] painClips;
    public AudioClip bucketClip;
    public AudioClip shearsClip;
    public AudioClip[] eatClips;
    public AudioClip drinkClip;
    public AudioClip eggDropClip;
    public AudioClip pregnantClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void SetClipAndPlay(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    AudioClip GetRandomClipFromList(AudioClip[] list)
    {
        if (list.Length == 0) return null;
        int randomIndex = Random.Range(0, list.Length);
        AudioClip selectedClip = list[randomIndex];
        return selectedClip;
    }

    public void Hurt()
    {
        SetClipAndPlay(GetRandomClipFromList(painClips));
    }

    public void Idle()
    {
        SetClipAndPlay(GetRandomClipFromList(idleClips));
    }

    public void Eat()
    {
        SetClipAndPlay(GetRandomClipFromList(eatClips));
    }

    public void Bucket()
    {
        SetClipAndPlay(bucketClip);
    }

    public void Shears()
    {
        SetClipAndPlay(shearsClip);
    }

    public void EggDrop()
    {
        SetClipAndPlay(eggDropClip);
    }

    public void Drink()
    {
        SetClipAndPlay(drinkClip);
    }

    public void Pregnant()
    {
        SetClipAndPlay(pregnantClip);
    }
}