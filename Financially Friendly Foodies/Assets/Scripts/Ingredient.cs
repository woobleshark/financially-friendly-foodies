using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Experimental.GraphView;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public enum IngredientType
{
    Bread,
    Cheese,
    Lettuce,
    Tomato,
    Egg,
    Ham,
    Bacon,
    Chicken
}

public class Ingredient : MonoBehaviour
{
    public IngredientType type;
    private Plate plate;

    private bool isDragging = false;
    private Vector3 offset;
    private Bounds plateBounds;

    private void Start()
    {
        plate = FindObjectOfType<Plate>();

        Collider2D plateCollider = plate.GetComponent<Collider2D>();
        if (plateCollider != null)
        {
            plateBounds = plateCollider.bounds;
        }
        else
        {
            Debug.LogWarning("Collider2D component not found on the plate.");
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            // Update the ingredient's position to follow the mouse.
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);
        }
    }

    private void OnMouseDown()
    {

        if (IsMouseWithinBounds(plateBounds))
        {
            // Start dragging if the click is within the bounds of a GameObject with a Plate component.
            isDragging = true;

            // Calculate the offset between the mouse click position and the ingredient's position.
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseUp()
    {
        // Stop dragging when the mouse button is released.
        isDragging = false;
        plate = null; // Clear the reference to the Plate component.

        if (!IsMouseWithinBounds(plateBounds))
        {
            Destroy(gameObject);
        }
    }

    private bool IsMouseWithinBounds(Bounds bounds)
    {
        // Check if the mouse position is within the object's bounds.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return bounds.Contains(mousePosition);
    }
}