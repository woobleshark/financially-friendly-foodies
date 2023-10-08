using TMPro;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public DataManager dataManager;

    public IngredientType type;
    public int quantity;

    public GameObject prefab;
    public TextMeshProUGUI qtyText;

    private void Start()
    {
        if (dataManager == null)
        {
            Debug.LogError("DataManager component not found on this GameObject.");
        }
        dataManager.ChangeItemsInInventory(type, 0);
    }
}