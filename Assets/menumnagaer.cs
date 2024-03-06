using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the game scene by name or index
        SceneManager.LoadScene("MainGameScene");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayGame();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitGame();
        }
    }

    public void QuitGame()
    {
        System.Diagnostics.Debug.Print("Quit game requested.");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Add this line to also stop play in the editor
#endif
    }
}
