using Animals;
using Animals.Produce;
using UnityEngine;

public class EggPickup : MonoBehaviour
{
    private ProduceItem eggProduce;
    private MoneyManager _moneyManager;
    private AnimalSoundController soundController;
    private ProducePopUp producePopUp;

    void Start()
    {
        _moneyManager = GameObject.Find("Money Manager").GetComponent<MoneyManager>();
        soundController = GetComponent<AnimalSoundController>();
        producePopUp = GameObject.Find("ProducePopUp").GetComponent<ProducePopUp>();
    }
    
    public void SetQuality(int quality)
    {
        eggProduce = new ProduceItem(ProduceType.Egg, quality);
    }
    void Update()
    {
        TapDetector.DetectTap(OnEggClicked, gameObject);
    }

    private void OnEggClicked()
    {
        _moneyManager.IncrementMoney(eggProduce.Value);
        producePopUp.SetAcquiredProduce(eggProduce);
        soundController.EggDrop();
        gameObject.transform.position = new Vector3(0, 20, 0);
        Destroy(gameObject, 2f);
    }
}
