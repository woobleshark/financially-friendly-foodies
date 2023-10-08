using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    private Collider2D plateCollider; // Assign the Collider2D in the Inspector.
    private List<GameObject> ingredientsInsidePlate = new List<GameObject>();

    private void Start()
    {
        plateCollider = GetComponent<Collider2D>();
    }

    public List<GameObject> GetIngredientsInsideCollider()
    {
        ingredientsInsidePlate.Clear(); // Clear the previous list.

        // Create a temporary list to store the colliders inside the triggerCollider.
        List<Collider2D> colliders = new List<Collider2D>();

        // Perform the overlap check and store the results in the 'colliders' list.
        plateCollider.OverlapCollider(new ContactFilter2D(), colliders);

        // Iterate through the colliders and add their GameObjects to the list.
        foreach (Collider2D collider in colliders)
        {
            GameObject objectInside = collider.gameObject;
            ingredientsInsidePlate.Add(objectInside);
        }

        return ingredientsInsidePlate;
        // Now, objectsInsideCollider contains the GameObjects inside the triggerCollider.
        // You can use this list for further processing or interaction.
    }
}