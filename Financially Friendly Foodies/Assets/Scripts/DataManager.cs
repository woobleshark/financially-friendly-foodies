using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public GameManager gameManager;

    public TextMeshProUGUI dayNumText;
    public TextMeshProUGUI moneyNumText;
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();

    public event Action OnInventoryChanged;

    private void Awake()
    {
        // Ensure there is only one instance of DataManager.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensures that the DataManager persists across scene changes.
        }
        else
        {
            Destroy(gameObject); // Destroy any additional instances.
        }

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

    public int GetItemsInInventory(string name)
    {
        InventoryItem item = inventoryItems.Find(i => i.name == name);
        return item != null ? item.quantity : 0;
    }

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
            GameObject newGameObject = new GameObject(Enum.GetName(typeof(IngredientType), ingredientType));
            InventoryItem newItem = newGameObject.AddComponent<InventoryItem>();
            newItem.type = ingredientType;
            newItem.quantity = quantity;
            newItem.itemName = Enum.GetName(typeof(IngredientType), ingredientType);

            inventoryItems.Add(newItem);
        }

        UpdateGameManagerInventory();

        // Trigger an event to notify UI updates.
        OnInventoryChanged?.Invoke();
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
