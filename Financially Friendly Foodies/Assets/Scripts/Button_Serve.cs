using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Serve : MonoBehaviour
{
    public Button serveButton;
    public Plate plate;
    public GameObject sandwich;

    private void Start()
    {
        serveButton.onClick.AddListener(Serve);
    }

    private void Serve()
    {
        List<GameObject> ingredientsInsidePlate = plate.GetIngredientsInsideCollider();

        foreach (GameObject obj in ingredientsInsidePlate)
        {
            Destroy(obj);
        }

        sandwich.SetActive(true);
    }
}