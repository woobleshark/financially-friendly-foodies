using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private GameManager gameManager;
    public DataManager dataManager;

    private static bool hasInitialized = false;

    private void Awake()
    {
        if (!hasInitialized)
        {
            InitializeGame();

            // Set the flag to true to prevent re-initialization.
            hasInitialized = true;

            // Make this GameObject persist throughout scene transitions.
            DontDestroyOnLoad(gameObject);
        }
    }

    private void InitializeGame()
    {
        // Find the GameManager instance in the scene.
        gameManager = GameManager.instance;

        if (gameManager != null)
        {
            // Set initial values for dayNumber and playerMoney.
            gameManager.dayNumber = 1;
            gameManager.playerMoney = 100;

            dataManager.Initialize();
            dataManager.UpdateDayCounterUI();
            dataManager.UpdateMoneyCounterUI();
        }
        else
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }
}
