using System.Collections;
using System.Collections.Generic;
using Animals.Produce;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SpriteEntry
{
    public ProduceType produceType;
    public Sprite sprite;
}

public class ProducePopUp : MonoBehaviour
{
    private TMP_Text produceValue;
    private Image produceIcon;

    private GameObject star1;
    private GameObject star2;
    private GameObject star3;

    [SerializeField] private List<SpriteEntry> spriteEntries;
    private Dictionary<ProduceType, Sprite> spriteDict;
    
    public float fadeDuration = 0.5f;
    public float displayDuration = 2f;
    
    private Queue<ProduceItem> produceQueue = new Queue<ProduceItem>();
    private bool isProcessing = false;
    

    void Start()
    {
        produceValue = Utility.FindChildByName(transform, "ProduceValue").GetComponent<TMP_Text>();
        produceIcon = Utility.FindChildByName(transform, "ProduceIcon").GetComponent<Image>();
        star1 = Utility.FindChildByName(transform, "Star1");
        star2 = Utility.FindChildByName(transform, "Star2");
        star3 = Utility.FindChildByName(transform, "Star3");
        InitDictionary();
        Disappear(false);
    }

    public void Interrupt()
    {
        StopAllCoroutines();
        produceQueue.Clear();
        isProcessing = false;
        Disappear(false);
    }
    
    public IEnumerator AppearAndDisappear()
    {
        Appear();
        yield return new WaitForSeconds(displayDuration);
        Disappear();
    }

    private void Appear(bool transition = true)
    {
        ChildElementsVisibility(true, transition);
    }

    public void Disappear(bool transition = true)
    {
        ChildElementsVisibility(false, transition);
    }
    
    void ChildElementsVisibility(bool visible, bool transition = true)
    {
        foreach (Transform child in transform)
        {
            var image = child.GetComponent<Image>();
            var text = child.GetComponent<TextMeshProUGUI>();
            if (image != null)
            {
                if (transition) StartCoroutine(FadeElement(image, !visible));
                else ElementVisibility(image, visible);
            }
            if (text != null)
            {
                if (transition) StartCoroutine(FadeElement(text, !visible));
                else ElementVisibility(text, visible);
            }
        }
    }

    private void ElementVisibility(Graphic element, bool visible)
    {
        Color color = element.color;
        color.a = visible ? 1 : 0;
        element.color = color;
    }

    private void InitDictionary()
    {
        spriteDict = new Dictionary<ProduceType, Sprite>();
        foreach (var entry in spriteEntries)
        {
            if (!spriteDict.ContainsKey(entry.produceType))
                spriteDict.Add(entry.produceType, entry.sprite);
        }
    }

    public void SetAcquiredProduce(ProduceItem produceItem)
    {
        produceQueue.Enqueue(produceItem);

        if (!isProcessing)
        {
            StartCoroutine(ProcessQueue());
        }
    }
    
    private IEnumerator ProcessQueue()
    {
        isProcessing = true;

        while (produceQueue.Count > 0)
        {
            var produceItem = produceQueue.Dequeue();
            DisplayProduceItem(produceItem);
            yield return new WaitForSeconds(displayDuration);
            Disappear();
            yield return new WaitForSeconds(fadeDuration);
        }

        isProcessing = false;
    }
    
    private void DisplayProduceItem(ProduceItem produceItem)
    {
        SetValue(produceItem.Value);
        SetSprite(produceItem.Type);
        SetQuality(produceItem.Quality);
        Appear(); // Show the popup
    }

    private void SetValue(int value)
    {
        produceValue.text = "+" + value;
    }

    private void SetSprite(ProduceType produceType)
    {
        if (spriteDict.TryGetValue(produceType, out var sprite))
        {
            produceIcon.sprite = sprite;
        }
        else
        {
            Debug.LogWarning($"No sprite found for produce type: {produceType}");
        }
    }

    private void SetQuality(int quality)
    {
        star1.SetActive(quality >= 1);
        star2.SetActive(quality >= 2);
        star3.SetActive(quality >= 3);
    }
    
    private IEnumerator FadeElement(Graphic element, bool fadeOut)
    {
        Color color = element.color;
        float startAlpha = fadeOut ? 1 : 0; 
        float targetAlpha = fadeOut ? 0 : 1;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);
            element.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        element.color = color;
    }
}
