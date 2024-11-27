using System.Collections;
using UnityEngine;

public class MilkManager : MonoBehaviour
{
    public float milkProductionTime = 30f;
    private bool hasMilk;
    private GameObject milkParticleSystem;

    void Start()
    {
        milkParticleSystem = Utility.FindChildByName(transform, "MilkParticleSystem");
        hasMilk = false;
        milkParticleSystem.SetActive(false);
        StartCoroutine(ProduceMilkAfterDelay(10));
    }

    private void SetMilk(bool active)
    {
        hasMilk = active;
    }

    public bool GetMilk()
    {
        if (hasMilk)
        {
            SetMilk(false);
            StartCoroutine(ProduceMilkAfterDelay(milkProductionTime));
            milkParticleSystem.SetActive(false);
            return true;
        }
        return false;
    }

    public void ProduceMilk()
    {
        SetMilk(true);
        milkParticleSystem.SetActive(true);
    }

    private IEnumerator ProduceMilkAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ProduceMilk();
    }
}
