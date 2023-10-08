using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

[Serializable]
public class IngredientDisplay
{
    public GameObject gameObject;
    public TextMeshProUGUI textDisplay;
    public IngredientType type;
}

public class UIManager : MonoBehaviour
{
    public List<IngredientDisplay> ingredientDisplayList;

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
            dataManager.ChangeItemsInInventory(item, 10);
        }
    }

    private void UpdateUI()
    {
        // Check if the DataManager reference is available.
        if (dataManager != null)
        {
            // Update the UI Text components with the current inventory quantities.
            int itemQty;

            foreach (IngredientDisplay item in ingredientDisplayList)
            {
                itemQty = dataManager.GetItemsInInventory(Enum.GetName(typeof(IngredientType), item.type));
                if (itemQty > 0)
                {
                    item.gameObject.SetActive(true);
                    item.textDisplay.text = itemQty.ToString();
                } else
                {
                    item.gameObject.SetActive(false);
                    item.textDisplay.text = "";
                }
            }
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
