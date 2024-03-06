using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInputHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu"); // Make sure to replace "MainMenu" with the exact name of your main menu scene
        }
    }
}
