using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Unity.VisualScripting;

public class BackgroundChanger : MonoBehaviour
{
    public Image backgroundImage; // Reference to the UI Image component.
    public Sprite[] backgroundImages; // Array of different background images.
    public string nextSceneName; // Name of the scene to load when the last image is reached.
    private int currentIndex = 0; // Index of the currently displayed background image.

    public void CycleBackground()
    {
        currentIndex = (currentIndex + 1) % backgroundImages.Length;
        backgroundImage.sprite = backgroundImages[currentIndex];

        // Check if the last image is reached.
        if (currentIndex == backgroundImages.Length - 1)
        {
            // Load the next scene.
            SceneManager.LoadScene(nextSceneName);
        }
    }
}