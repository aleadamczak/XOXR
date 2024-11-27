using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int money;
    public TMP_Text mainScreenText;
    public TMP_Text shopText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            IncrementMoney(100);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            DecrementMoney(100);
        }
    }

    public void UpdateTextFields()
    {
        mainScreenText.text = money.ToString();
        shopText.text = money.ToString();
    }

    public void IncrementMoney(int howMuch)
    {
        money += howMuch;
        UpdateTextFields();
    }

    public void DecrementMoney(int howMuch)
    {
        int nextMoney = money - howMuch;
        if (nextMoney < 0)
        {
            Debug.Log("Could not decrement that much money, balance would go under zero!");
        }
        else
        {
            money = nextMoney;
        }

        UpdateTextFields();
    }
}