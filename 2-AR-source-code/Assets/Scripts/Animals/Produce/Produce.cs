using Animals;
using Animals.Produce;
using UnityEngine;

public class Produce : MonoBehaviour
{
    protected DeathManager deathManager;
    private hungerController hungerController;
    private FurManager furManager;
    private MilkManager milkManager;

    public ProduceType swordProduce = ProduceType.Nothing;
    public ProduceType bucketProduce = ProduceType.Nothing;
    public ProduceType shearsProduce = ProduceType.Nothing;

    private int produceQuality;
    private MoneyManager _moneyManager;

    private GUIManager guiManager;
    private AnimalSoundController soundController;
    private ProducePopUp producePopUp;

    void Start()
    {
        deathManager = GetComponent<DeathManager>();
        hungerController = GetComponent<hungerController>();
        GameObject moneyManagerGo = GameObject.Find("Money Manager");
        GameObject guiManagerGo = GameObject.Find("GUI Manager");
        _moneyManager = moneyManagerGo.GetComponent<MoneyManager>();
        guiManager = guiManagerGo.GetComponent<GUIManager>();
        furManager = GetComponent<FurManager>();
        milkManager = GetComponent<MilkManager>();
        soundController = GetComponent<AnimalSoundController>();
        producePopUp = GameObject.Find("ProducePopUp").GetComponent<ProducePopUp>();
    }

    void Update()
    {
        TapDetector.DetectTap(GetProduce, gameObject);
    }

    public int GetProduceQuality()
    {
        int hunger = hungerController.hunger;
        int thirst = hungerController.thirst;

        if (hunger > 50 && thirst > 50)
        {
            return 3;
        }

        if (hunger < 30 || thirst < 30)
        {
            return 1;
        }

        return 2;
    }

    bool IsToolSelected()
    {
        switch (guiManager.selectedItem)
        {
            case Items.Bucket:
            case Items.Shears:
            case Items.Sword:
                return true;
            default:
                return false;
        }
    }

    void GetProduce()
    {
        if (!IsToolSelected()) return;
        produceQuality = GetProduceQuality();
        ProduceItem produceObtained = new ProduceItem(ProduceType.Nothing, 0);
        switch (guiManager.selectedItem)
        {
            case Items.Nothing:
                break;
            case Items.Sword:
                produceObtained = UseSword();
                break;
            case Items.Bucket:
                produceObtained = UseBucket();
                break;
            case Items.Shears:
                produceObtained = UseShears();
                break;
        }

        if (produceObtained.Type != ProduceType.Nothing)
        {
            _moneyManager.IncrementMoney(produceObtained.Value);
            producePopUp.SetAcquiredProduce(produceObtained);
        }
    }

    public ProduceItem UseSword()
    {
        ProduceItem meat = new ProduceItem(swordProduce, produceQuality);
        deathManager.Die();
        return meat;
    }

    public ProduceItem UseBucket()
    {
        if (bucketProduce == ProduceType.Nothing)
        {
            return new ProduceItem(ProduceType.Nothing, 0);
        }

        if (milkManager.GetMilk())
        {
            soundController.Bucket();
            return new ProduceItem(bucketProduce, produceQuality);
        }
        return new ProduceItem(ProduceType.Nothing, 0);
    }

    public ProduceItem UseShears()
    {
        if (shearsProduce == ProduceType.Nothing)
        {
            return new ProduceItem(ProduceType.Nothing, 0);
        }

        if (furManager.RemoveFur())
        {
            soundController.Shears();
            return new ProduceItem(shearsProduce, produceQuality);
        }

        return new ProduceItem(ProduceType.Nothing, 0);
    }
}