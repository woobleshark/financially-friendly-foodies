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
}