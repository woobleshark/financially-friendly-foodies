using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_StartDay : MonoBehaviour
{
    private GameManager gameManager;

    public Image overlay;
    public Button startButton;
    public List<GameObject> preStartList;
    public List<GameObject> postStartList;
    public GameObject sandwich;

    private bool dayStarted = false;

    private void Start()
    {
        gameManager = GameManager.instance;

        // Enable the overlay.
        overlay.gameObject.SetActive(true);

        // Add a click event listener to the start button.
        startButton.onClick.AddListener(StartGame);

        foreach(GameObject obj in preStartList)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in postStartList)
        {
            obj.SetActive(false);
        }

        sandwich.SetActive(false);
    }

    private void Update()
    {
        // Disable interactions if the game hasn't started.
        if (!dayStarted)
        {
            // Prevent raycasting / clicking on objects behind the panel.
            overlay.raycastTarget = false;
        }
        else
        {
            // Enable interactions once the game has started.
            overlay.raycastTarget = true;
        }
    }

    private void StartGame()
    {
        // Disable the overlay.
        overlay.gameObject.SetActive(false);

        // You can add additional logic here to start the game.
        // For example, load a new scene or enable game objects.

        // Set the gameStarted flag to true to enable interactions.
        dayStarted = true;

        foreach (GameObject obj in preStartList)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in postStartList)
        {
            obj.SetActive(true);
        }

        gameManager.SetGameState(GameState.Cooking);
    }
}