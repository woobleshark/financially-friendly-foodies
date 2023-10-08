using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    OtherMenu,
    Cooking,
    Shopping,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState CurrentState { get; private set; }

    public int dayNumber;
    public int playerMoney;
    public List<InventoryItem> gameInventory = new List<InventoryItem>();

    // Add more data as needed.

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetGameState(GameState.MainMenu);
    }

    public void SetGameState(GameState newState)
    {
        CurrentState = newState;
    }
}