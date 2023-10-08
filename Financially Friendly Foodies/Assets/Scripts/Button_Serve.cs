using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Serve : MonoBehaviour
{
    public Button serveButton;
    public Plate plate;
    public GameObject sandwich;

    public float displayDuration = 1.0f;

    private void Start()
    {
        serveButton.onClick.AddListener(Serve);
    }

    private void Serve()
    {
        List<GameObject> ingredientsInsidePlate = plate.GetIngredientsInsideCollider();

        if (ingredientsInsidePlate.Count > 1)
        {
            StartCoroutine(DisplaySandwich());
        }

        foreach (GameObject obj in ingredientsInsidePlate)
        {
            Destroy(obj);
        }
    }

    private IEnumerator DisplaySandwich()
    {
        // Enable the sandwich GameObject.
        sandwich.SetActive(true);

        // Wait for the specified duration.
        yield return new WaitForSeconds(displayDuration);

        // Disable the sandwich GameObject after the duration has elapsed.
        sandwich.SetActive(false);
    }
}