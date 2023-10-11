using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CustomerRequest : MonoBehaviour
{
    public GameObject[] ingredientPrefabs; // Assign the ingredient prefabs in the Inspector.

    private GameObject[] instantiatedIngredients; // Array to store references to instantiated ingredients.
    public List<IngredientType> instantiatedIngredientTypes = new List<IngredientType>();

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void DisplaySelectedIngredients()
    {
        gameObject.SetActive(true);

        // Randomly pick 2-3 ingredient prefabs.
        int ingredientCount = Random.Range(2, 4);
        instantiatedIngredients = new GameObject[ingredientCount];

        for (int i = 0; i < ingredientCount; i++)
        {
            int randomIndex = Random.Range(0, ingredientPrefabs.Length);
            GameObject selectedIngredientPrefab = ingredientPrefabs[randomIndex];
            instantiatedIngredientTypes.Add(selectedIngredientPrefab.GetComponent<Ingredient>().type);

            if (selectedIngredientPrefab != null)
            {
                // Calculate the position to place the ingredient prefab within the customer's space.
                Vector3 position = transform.position + new Vector3(i * 250.0f - ingredientCount * 75f, 100, 0); // Adjust the position as needed.

                // Place the selected ingredient prefab within the customer's space.
                GameObject ingredientInstance = Instantiate(selectedIngredientPrefab, position, Quaternion.identity, transform);
                instantiatedIngredients[i] = ingredientInstance;
            }
        }
    }

    public void DestroyRequest()
    {
        // Loop through the instantiated ingredients and destroy them.
        if (instantiatedIngredients != null)
        {
            foreach (GameObject ingredient in instantiatedIngredients)
            {
                if (ingredient != null)
                {
                    Destroy(ingredient);
                }
            }
        }

        instantiatedIngredientTypes.Clear();
        gameObject.SetActive(false);
    }
}