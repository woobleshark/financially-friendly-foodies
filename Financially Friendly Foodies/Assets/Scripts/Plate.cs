using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    private Collider2D plateCollider; // Assign the Collider2D in the Inspector.
    public List<GameObject> ingredientsOnPlate = new List<GameObject>();
    private List<IngredientType> ingredientTypesOnPlate = new List<IngredientType>();

    private void Start()
    {
        plateCollider = GetComponent<Collider2D>();
    }

    public List<IngredientType> GetIngredientsInsideCollider()
    {
        ingredientsOnPlate.Clear(); // Clear the previous list.
        ingredientTypesOnPlate.Clear();

        // Create a temporary list to store the colliders inside the triggerCollider.
        List<Collider2D> colliders = new List<Collider2D>();

        // Perform the overlap check and store the results in the 'colliders' list.
        plateCollider.OverlapCollider(new ContactFilter2D(), colliders);

        // Iterate through the colliders and add their GameObjects to the list.
        foreach (Collider2D collider in colliders)
        {
            GameObject objectInside = collider.gameObject;
            if (objectInside.CompareTag("Ingredient"))
            {
                ingredientsOnPlate.Add(objectInside);
                ingredientTypesOnPlate.Add(objectInside.GetComponent<Ingredient>().type);
            }
        }

        return ingredientTypesOnPlate;
        // Now, objectsInsideCollider contains the GameObjects inside the triggerCollider.
        // You can use this list for further processing or interaction.
    }

    public bool IngredientsMatch(List<IngredientType> expectedIngredients)
    {
        // Compare the expected and actual ingredient lists.
        if (expectedIngredients.Count != ingredientTypesOnPlate.Count)
        {
            return false;
        }

        List<IngredientType> foundExpectedIngredients = new List<IngredientType>(expectedIngredients);

        foreach (IngredientType ingredient in ingredientTypesOnPlate)
        {
                // Check if the actual ingredient type is one of the expected ingredients.
                if (foundExpectedIngredients.Contains(ingredient))
                {
                    // Remove the found ingredient from the list.
                    foundExpectedIngredients.Remove(ingredient);
                }
                else
                {
                    return false; // Ingredient type not expected.
                }
        }

        return foundExpectedIngredients.Count == 0;
    }
}