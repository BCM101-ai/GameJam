using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClearUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject stageClearPanel;

    void Start()
    {
        // Show the Stage Clear UI right away
        stageClearPanel.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextStage(string nextSceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Poupav");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
