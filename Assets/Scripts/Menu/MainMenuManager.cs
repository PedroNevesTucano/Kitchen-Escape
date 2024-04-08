using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the LevelTest scene
        SceneManager.LoadScene("LevelTest");
    }

    public void ExitGame()
    {
        // Quit the application
        Application.Quit();
#if UNITY_EDITOR
        // If running in the Unity Editor, stop playing.
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public GameObject exitConfirmationPanel; // Assign in the Inspector

    public void ShowExitConfirmation()
    {
        exitConfirmationPanel.SetActive(true); // Show the confirmation pop-up
    }

    public void ExitConfirmation(bool confirmExit)
    {
        if (confirmExit)
        {
            // If "Yes" is clicked
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // For editor mode
#endif
        }
        else
        {
            // If "No" is clicked, hide the confirmation and possibly return to the menu
            exitConfirmationPanel.SetActive(false);
        }
    }

}
