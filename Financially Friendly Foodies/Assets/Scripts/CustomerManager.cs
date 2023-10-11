using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    public CustomerRequest requests;
    public GameObject[] customerPrefabs; // Array of different customer prefabs.
    public Plate plate;
    public GameObject heartReact;
    public GameObject angryReact;
    public Vector3 position = new Vector3(-315, 395, 0);

    private GameObject currentCustomer;
    private GameObject currentRequest;
    private GameObject reaction;
    private int customerIndex = 0;
    private bool serving = false;

    public Button serveButton; // Reference to the UI Button for serving.

    private void Start()
    {
        // Set up the button click event.
        serveButton.onClick.AddListener(ServeCustomer);

        // Spawns customers after Start button is clicked.
    }

    public IEnumerator SpawnCustomers()
    {
        while (true)
        {
            // Wait for 3 seconds before spawning a customer.
            yield return new WaitForSeconds(1f);

            // Spawn the current customer prefab.
            currentCustomer = Instantiate(customerPrefabs[customerIndex], transform.position, Quaternion.identity);

            // Display the selected ingredients on this game object (customer).
            requests.DisplaySelectedIngredients();

            // Set the serving flag to true.
            serving = true;

            // Wait for the serve button to be pressed.
            while (serving)
            {
                yield return null; // Wait for the next frame.
            }

            if (plate.IngredientsMatch(requests.instantiatedIngredientTypes))
            {
                reaction = Instantiate(heartReact, position, Quaternion.identity, transform);
            }
            else
            {
                reaction = Instantiate(angryReact, position, Quaternion.identity, transform);
            }

            yield return new WaitForSeconds(2f);

            Destroy(reaction);

            // Deactivate the current customer.
            Destroy(currentCustomer);
            requests.DestroyRequest();

            // Switch to the next customer prefab (cycling between A and B).
            customerIndex = (customerIndex + 1) % customerPrefabs.Length;
        }
    }

    public void ServeCustomer()
    {
        if (serving)
        {
            // Set the serving flag to false to continue with the next customer.
            serving = false;
        }
    }
}