using Unity.VisualScripting;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    public GameObject ingredientPrefab; // Assign your ingredient prefab in the Inspector.
    public GameObject plate;

    private GameObject currentIngredient;
    private bool isDragging = false;
    private Vector3 offset;

    private Bounds itemBounds;
    private Bounds boxBounds;
    private Bounds plateBounds;

    private void Start()
    {
        Collider2D collider = GetComponent<Collider2D>();

        if (collider != null)
        {
            itemBounds = collider.bounds;
        }
        else
        {
            Debug.LogWarning("Collider2D component not found.");
        }

        Collider2D plateCollider = plate.GetComponent<Collider2D>();
        if (plateCollider != null)
        {
            plateBounds = plateCollider.bounds;
        }
        else
        {
            Debug.LogWarning("Collider2D component not found on the plate.");
        }

        boxBounds = ExpandBounds(itemBounds, 100.0f);
    }

    private void Update()
    {
        // Check for mouse down to spawn a new ingredient prefab.
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseWithinBounds(itemBounds))
            {
                SpawnIngredient();
            }
        }

        // Continue with drag-and-drop logic if an ingredient is spawned.
        if (currentIngredient != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Calculate the offset between the mouse click position and the ingredient's position.
                offset = currentIngredient.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isDragging = true;
            }

            if(Input.GetMouseButtonUp(0))
            {
                isDragging = false;

                // Check if the ingredient is dropped outside the plate's bounds.

                if (!IsMouseWithinBounds(boxBounds))
                {
                    DataManager.Instance.ChangeItemsInInventory(ingredientPrefab.GetComponent<Ingredient>().type, -1);
                }

                if (!IsMouseWithinBounds(plateBounds))
                {
                    Destroy(currentIngredient);
                }

                currentIngredient = null;
            }

            if (isDragging)
            {
                // Update the ingredient's position based on the mouse position.
                Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
                currentIngredient.transform.position = new Vector3(newPosition.x, newPosition.y, currentIngredient.transform.position.z);
            }
        }
    }

    Bounds ExpandBounds(Bounds originalBounds, float expansionAmount)
    {
        // Clone the original bounds.
        Bounds expandedBounds = new Bounds(originalBounds.center, originalBounds.size);

        // Increase the size of the expanded bounds by the expansionAmount.
        expandedBounds.Expand(expansionAmount);

        return expandedBounds;
    }

    private bool IsMouseWithinBounds(Bounds bounds)
    {
        // Check if the mouse position is within the object's bounds.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return bounds.Contains(mousePosition);
    }

    private void SpawnIngredient()
    {
        // Spawn a new ingredient prefab at the spawn point.
        if (ingredientPrefab != null)
        {
            currentIngredient = Instantiate(ingredientPrefab, transform.position, Quaternion.identity);
        }
    }
}