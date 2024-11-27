using System.Collections;
using UnityEngine;

public class FurManager : MonoBehaviour
{
    public float furRegrowthTime = 60f;
    private GameObject bodyFur;
    private GameObject headFur;
    private GameObject BLLegFur;
    private GameObject BRLegFur;
    private GameObject FLLegFur;
    private GameObject FRLegFur;
    private bool hasFur;

    void Start()
    {
        hasFur = true;
        bodyFur = GetFurBodyPart("Body");
        headFur = GetFurBodyPart("Head");
        BLLegFur = GetFurBodyPart("LegBL");
        BRLegFur = GetFurBodyPart("LegBR");
        FLLegFur = GetFurBodyPart("LegFL");
        FRLegFur = GetFurBodyPart("LegFR");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            RemoveFur();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GrowFur();
        }
    }

    private void SetFur(bool active)
    {
        hasFur = active;
        bodyFur.SetActive(active);
        headFur.SetActive(active);
        BLLegFur.SetActive(active);
        BRLegFur.SetActive(active);
        FLLegFur.SetActive(active);
        FRLegFur.SetActive(active);
    }

    public bool RemoveFur()
    {
        if (hasFur)
        {
            SetFur(false);
            StartCoroutine(RegrowFurAfterDelay(furRegrowthTime));
            return true;
        }
        return false;
    }

    public void GrowFur()
    {
        SetFur(true);
    }

    private IEnumerator RegrowFurAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GrowFur();
    }

    GameObject GetFurBodyPart(string bodyPart)
    {
        string stringLocator = $"{bodyPart}_Fur";
        return Utility.FindChildByName(transform, stringLocator);
    }
}