using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;


public class ShopManager : MonoBehaviour
{
    public SceneInitializer sceneInitializer;

    // Buttons for each animal
    public Button sheepButton;
    public Button cowButton;
    public Button pigButton;
    public Button chickenButton;

    // Delete buttons for each animal
    public Button sheepDeleteButton;
    public Button cowDeleteButton;
    public Button pigDeleteButton;
    public Button chickenDeleteButton;

    // Buy items button
    public Button buyItemsButton;

    // TextMeshPro Text elements for displaying quantities
    public TMP_Text sheepQuantityText;
    public TMP_Text cowQuantityText;
    public TMP_Text pigQuantityText;
    public TMP_Text chickenQuantityText;

    private int sheepPrice = 100;
    private int pigPrice = 100;
    private int chickenPrice = 100;
    private int cowPrice = 200;

    private int sheepQuantity = 0;
    private int pigQuantity = 0;
    private int chickenQuantity = 0;
    private int cowQuantity = 0;

    // TextMeshPro Text elements for displaying prices
    public TMP_Text sheepPriceText;
    public TMP_Text cowPriceText;
    public TMP_Text pigPriceText;
    public TMP_Text chickenPriceText;

    // TextMeshPro Text for basket total
    public TMP_Text basketTotalText;

    // Coins TextMeshPro text (to display and retrieve player coins)
    public TMP_Text playerCoinsText;

    // Player's total coins
    public MoneyManager playerMoney;
    private Dictionary<string, int> basketItems = new Dictionary<string, int>();
    private int basketQuantity = 0;

    private ShopAudioManager audioManager;

    private void Start()
    {
        // Assign button click events for adding animals
        sheepButton.onClick.AddListener(() => OnAnimalButtonClick("Sheep"));
        cowButton.onClick.AddListener(() => OnAnimalButtonClick("Cow"));
        pigButton.onClick.AddListener(() => OnAnimalButtonClick("Pig"));
        chickenButton.onClick.AddListener(() => OnAnimalButtonClick("Chicken"));

        // Assign button click events for deleting animals
        sheepDeleteButton.onClick.AddListener(() => OnDeleteItemButtonClick("Sheep"));
        cowDeleteButton.onClick.AddListener(() => OnDeleteItemButtonClick("Cow"));
        pigDeleteButton.onClick.AddListener(() => OnDeleteItemButtonClick("Pig"));
        chickenDeleteButton.onClick.AddListener(() => OnDeleteItemButtonClick("Chicken"));

        // Assign buy items button event
        buyItemsButton.onClick.AddListener(OnBuyItemsButtonClick);

        InitializeQuantities();

        // Update the button states based on coins and quantities
        UpdateDeleteButtonStates();
        UpdateBuyButtonState();
        UpdateBasketTotalText();
        SavePlayerCoinsToText();
        InitializeBasket();

        audioManager = GetComponent<ShopAudioManager>();
    }

    public void InitializeBasket()
    {
        basketItems.Add("Sheep", 0);
        basketItems.Add("Cow", 0);
        basketItems.Add("Pig", 0);
        basketItems.Add("Chicken", 0);
    }

    private void SavePlayerCoinsToText()
    {
        playerCoinsText.text = playerMoney.money.ToString();
    }

    private void InitializeQuantities()
    {
        sheepQuantity = 0;
        pigQuantity = 0;
        chickenQuantity = 0;
        cowQuantity = 0;

        sheepPriceText.text = sheepPrice.ToString();
        pigPriceText.text = pigPrice.ToString();
        chickenPriceText.text = chickenPrice.ToString();
        cowPriceText.text = cowPrice.ToString();
        sheepQuantityText.text = sheepQuantity.ToString();
        cowQuantityText.text = cowQuantity.ToString();
        pigQuantityText.text = pigQuantity.ToString();
        chickenQuantityText.text = chickenQuantity.ToString();
    }

    private int FundsAvailable()
    {
        return playerMoney.money - CalculateBasketTotal();
    }

    private bool CanAfford(string animal)
    {
        switch (animal.ToLower())
        {
            case "sheep":
                return FundsAvailable() >= sheepPrice;
            case "chicken":
                return FundsAvailable() >= chickenPrice;
            case "pig":
                return FundsAvailable() >= pigPrice;
            case "cow":
                return FundsAvailable() >= cowPrice;
            default:
                return false;
        }
    }

    // Method for handling animal button clicks (increase quantity)
    public void OnAnimalButtonClick(string animal)
    {
        if (CanAfford(animal))
        {
            switch (animal)
            {
                case "Sheep":
                    UpdateQuantity(sheepQuantityText, ref sheepQuantity, 1);
                    break;
                case "Cow":
                    UpdateQuantity(cowQuantityText, ref cowQuantity, 1);
                    break;
                case "Pig":
                    UpdateQuantity(pigQuantityText, ref pigQuantity, 1);
                    break;
                case "Chicken":
                    UpdateQuantity(chickenQuantityText, ref chickenQuantity, 1);
                    break;
            }

            // Update button states
            UpdateDeleteButtonStates();
            UpdateBuyButtonState();
            UpdateBasketTotalText();
            UpdateBasketItems(animal);
            basketQuantity++;
        }
    }

    // Method for handling delete button clicks (decrease quantity)
    public void OnDeleteItemButtonClick(string animal)
    {
        switch (animal)
        {
            case "Sheep":
                UpdateQuantity(sheepQuantityText, ref sheepQuantity, -1);
                break;
            case "Cow":
                UpdateQuantity(cowQuantityText, ref cowQuantity, -1);
                break;
            case "Pig":
                UpdateQuantity(pigQuantityText, ref pigQuantity, -1);
                break;
            case "Chicken":
                UpdateQuantity(chickenQuantityText, ref chickenQuantity, -1);
                break;
        }

        // Update button states
        UpdateDeleteButtonStates();
        UpdateBuyButtonState();
        UpdateBasketTotalText();
        UpdateBasketItems(animal);
        basketQuantity--;
    }

    // Helper method to update the quantity by adding or subtracting
    private void UpdateQuantity(TMP_Text quantityText, ref int quantityInt, int change)
    {
        int currentQuantity = int.Parse(quantityText.text);
        currentQuantity = Mathf.Max(0, currentQuantity + change);
        quantityInt = currentQuantity;
        quantityText.text = currentQuantity.ToString();
    }

    // Method to update the interactability of delete buttons
    private void UpdateDeleteButtonStates()
    {
        sheepDeleteButton.interactable = int.Parse(sheepQuantityText.text) > 0;
        cowDeleteButton.interactable = int.Parse(cowQuantityText.text) > 0;
        pigDeleteButton.interactable = int.Parse(pigQuantityText.text) > 0;
        chickenDeleteButton.interactable = int.Parse(chickenQuantityText.text) > 0;
    }

    // Method to calculate the total cost of animals currently in the basket
    private int CalculateBasketTotal()
    {
        int sheepCost = sheepQuantity * sheepPrice;
        int cowCost = cowQuantity * cowPrice;
        int pigCost = pigQuantity * pigPrice;
        int chickenCost = chickenQuantity * chickenPrice;

        return sheepCost + cowCost + pigCost + chickenCost;
    }

    private void UpdateBasketItems(string animal)
    {
        switch (animal)
        {
            case "Sheep":
                if (sheepQuantity > 0)
                    basketItems["Sheep"] = sheepQuantity;
                else
                    basketItems.Remove("Sheep");
                break;
            case "Cow":
                if (cowQuantity > 0)
                    basketItems["Cow"] = cowQuantity;
                else
                    basketItems.Remove("Cow");
                break;
            case "Pig":
                if (pigQuantity > 0)
                    basketItems["Pig"] = pigQuantity;
                else
                    basketItems.Remove("Pig");
                break;
            case "Chicken":
                if (chickenQuantity > 0)
                    basketItems["Chicken"] = chickenQuantity;
                else
                    basketItems.Remove("Chicken");
                break;
        }
    }

    public Dictionary<string, int> GetBasketItems()
    {
        return basketItems;
    }

    private void UpdateBasketTotalText()
    {
        int basketTotal = CalculateBasketTotal();
        basketTotalText.text = basketTotal.ToString();
    }

    private void UpdateBuyButtonState()
    {
        int totalCost = CalculateBasketTotal();
        int basketQuantities = chickenQuantity + pigQuantity + cowQuantity + sheepQuantity;
        buyItemsButton.interactable =
            playerMoney.money >= totalCost && basketQuantities > 0; // Only enable if player can afford it
    }

    // Method to handle buying items
    public void OnBuyItemsButtonClick()
    {
        int totalCost = CalculateBasketTotal();

        if (playerMoney.money >= totalCost) // Check if player has enough coins
        {
            playerMoney.money -= totalCost;
            playerCoinsText.text = playerMoney.money.ToString(); // Update the coin display

            // Reset all quantities to 0 after purchase

            sceneInitializer.setAnimalsToBePlaced(basketQuantity);
            basketQuantity = 0;
            InitializeQuantities();

            // Save the player's coins to the text
            SavePlayerCoinsToText();

            // Update button states after buying
            UpdateDeleteButtonStates();
            UpdateBuyButtonState();
            UpdateBasketTotalText(); // Reset basket total after purchase
            sceneInitializer.EnterPlacingMode();
            audioManager.PlayAudio();
        }
        else
        {
            Debug.Log("Not enough coins to buy items");
        }
    }

    public void ClearBasket()
    {
        basketItems["Sheep"] = 0;
        basketItems["Pig"] = 0;
        basketItems["Cow"] = 0;
        basketItems["Chicken"] = 0;
    }
}