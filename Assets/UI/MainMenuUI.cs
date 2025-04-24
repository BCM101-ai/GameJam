using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject settingsPanel;

    // Start game and load Level_1 scene
    public void StartGame()
    {
        SceneManager.LoadScene("Da");
    }

    // Show settings panel
    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Settings Panel not assigned!");
        }
    }

    // Hide settings panel
    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }

    // Quit the game
    public void QuitGame()
    {
        Debug.Log("Quit button clicked");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
