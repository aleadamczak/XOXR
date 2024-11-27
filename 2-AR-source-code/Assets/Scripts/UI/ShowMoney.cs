using TMPro;
using UnityEngine;

public class ShowMoney : MonoBehaviour
{
    private TMP_Text playerCoinsText;
    public MoneyManager playerMoney;

    void OnEnable()
    {
        if (playerCoinsText && playerMoney)
        {
            UpdateCoinDisplay();
        }
    }

    void Start()
    {
        playerCoinsText = GetComponent<TMP_Text>();
        UpdateCoinDisplay();
    }

    public void UpdateCoinDisplay()
    {
        playerCoinsText.text = playerMoney.money.ToString();
    }
}
