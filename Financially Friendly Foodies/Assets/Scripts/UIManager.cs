using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI item1QuantityText;
    public TextMeshProUGUI item2QuantityText;
    public TextMeshProUGUI item3QuantityText;
    public TextMeshProUGUI item4QuantityText;
    public TextMeshProUGUI item5QuantityText;
    public TextMeshProUGUI item6QuantityText;
    public TextMeshProUGUI item7QuantityText;
    public TextMeshProUGUI item8QuantityText;

    private DataManager dataManager; // Reference to the DataManager.

    private void Start()
    {
        // Find and store a reference to the DataManager in the scene.
        dataManager = FindObjectOfType<DataManager>();

        // Subscribe to the DataManager's OnInventoryChanged event.
        if (dataManager != null)
        {
            dataManager.OnInventoryChanged += UpdateUI;
        }

        foreach (IngredientType item in Enum.GetValues(typeof(IngredientType)))
        {
            dataManager.ChangeItemsInInventory(item, 0);
        }
    }

    private void UpdateUI()
    {
        Debug.Log("UpdateUI called");
        // Check if the DataManager reference is available.
        if (dataManager != null)
        {
            // Update the UI Text components with the current inventory quantities.
            item1QuantityText.text = dataManager.GetItemsInInventory("Bread").ToString();
            item2QuantityText.text = dataManager.GetItemsInInventory("Cheese").ToString();
            item3QuantityText.text = dataManager.GetItemsInInventory("Lettuce").ToString();
            item4QuantityText.text = dataManager.GetItemsInInventory("Tomato").ToString();
            item5QuantityText.text = dataManager.GetItemsInInventory("Egg").ToString();
            item6QuantityText.text = dataManager.GetItemsInInventory("Ham").ToString();
            item7QuantityText.text = dataManager.GetItemsInInventory("Bacon").ToString();
            item8QuantityText.text = dataManager.GetItemsInInventory("Chicken").ToString();
        }
        else {
            Debug.LogError("DataManager not found in the scene.");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when the UIManager is destroyed.
        if (dataManager != null)
        {
            dataManager.OnInventoryChanged -= UpdateUI;
        }
    }
}
