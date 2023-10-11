using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName; // Assign the name of the scene to load in the Inspector.

    public void ChangeScene()
    {
        // Load the specified scene.
        SceneManager.LoadScene(sceneName);
    }
}