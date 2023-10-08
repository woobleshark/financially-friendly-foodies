using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public GameManager gameManager;

    public TextMeshProUGUI dayNumText;
    public TextMeshProUGUI moneyNumText;
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();

    public void Initialize()
    {
        gameManager = GameManager.instance;

        if (gameManager == null)
        {
            Debug.LogError("GameManager is missing!");
        }
    }


    /** DAY COUNTER MANAGER **/

    public void UpdateDayCounter()
    {
            // Increment the day number.
            gameManager.dayNumber++;

            // Update any UI or text elements displaying the day number.
            UpdateDayCounterUI();
    }

    public void UpdateDayCounterUI()
    {
        dayNumText.text = "Day " + gameManager.dayNumber.ToString();
    }


    /** PLAYER MONEY MANAGER **/

    public void UpdatePlayerMoney(int amount)
    {
        gameManager.playerMoney += amount;
        UpdateMoneyCounterUI();
    }

    public void UpdateMoneyCounterUI()
    {
        moneyNumText.text = "$" + gameManager.playerMoney.ToString();
    }


    /** INVENTORY MANAGER **/

    public void ChangeItemsInInventory(IngredientType ingredientType, int quantity)
    {
        // Check if the ingredient type already exists in the inventory.
        InventoryItem existingItem = inventoryItems.Find(item => item.type == ingredientType);

        if (existingItem != null)
        {
            // If it exists, update the quantity.
            existingItem.quantity += quantity;
        }
        else
        {
            // If it doesn't exist, create a new InventoryItem.
            GameObject newGameObject = new GameObject(ingredientType.ToString());
            InventoryItem newItem = newGameObject.AddComponent<InventoryItem>();
            newItem.type = ingredientType;
            newItem.quantity = quantity;

            inventoryItems.Add(newItem);
        }

        UpdateGameManagerInventory();
    }

    // Stores inventory in GameManager, which transfers between scenes
    public void UpdateGameManagerInventory()
    {
        GameManager.instance.gameInventory.Clear();

        foreach (InventoryItem item in inventoryItems)
        {
            GameManager.instance.gameInventory.Add(item);
        }
    }

    // Add methods to handle ingredient amounts, day progression, etc.
}
