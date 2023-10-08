using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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

    // Add methods to modify and retrieve game data.
}