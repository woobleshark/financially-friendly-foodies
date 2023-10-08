using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingStation : MonoBehaviour
{
    private List<Ingredient> ingredientsOnStation = new List<Ingredient>();
    public Recipe currentRecipe; // You'll need to create a Recipe class (see Step 3).

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ingredient"))
        {
            Ingredient ingredient = other.GetComponent<Ingredient>();
            if (ingredient != null)
            {
                ingredientsOnStation.Add(ingredient);
                // You may want to visually show that the ingredient has been added to the station.
            }
        }
    }

    public void Cook()
    {
            if (IsRecipeMatched())
            {
                StartCooking();
            }
            else
            {
                // Display a message to the player that the recipe doesn't match.
                Debug.Log("Recipe does not match the ingredients!");
            }
    }

    private bool IsRecipeMatched()
    {
        if (currentRecipe == null)
            return false;

        HashSet<IngredientType> collectedIngredientTypes = new HashSet<IngredientType>();
        foreach (Ingredient ingredient in ingredientsOnStation)
        {
            collectedIngredientTypes.Add(ingredient.type);
        }

        foreach (IngredientType requiredIngredient in currentRecipe.requiredIngredients)
        {
            if (!collectedIngredientTypes.Contains(requiredIngredient))
            {
                return false;
            }
        }

        return true;
    }

    private void StartCooking()
    {
        // Implement cooking logic here.
        // You can use timers or other methods to simulate cooking.
        // When cooking is complete, finish the recipe.
    }
}
