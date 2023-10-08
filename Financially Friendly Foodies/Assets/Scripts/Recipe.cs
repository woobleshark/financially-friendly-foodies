using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Recipe
{
    public string recipeName;
    public List<IngredientType> requiredIngredients;
    public GameObject resultDish; // Prefab for the cooked dish.

    private List<IngredientType> collectedIngredients = new List<IngredientType>();

    public bool CanAddIngredient(Ingredient ingredient)
    {
        return requiredIngredients.Contains(ingredient.type) && !collectedIngredients.Contains(ingredient.type);
    }

    public void AddIngredient(Ingredient ingredient)
    {
        collectedIngredients.Add(ingredient.type);
    }

    public bool IsComplete()
    {
        return collectedIngredients.Count == requiredIngredients.Count;
    }
}