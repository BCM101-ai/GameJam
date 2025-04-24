// เปลี่ยน line 56,59 เป็นชื่อ scene ด่าน 2
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageClearUI : MonoBehaviour
{
    [Header("UI References")]
    public Image whiteOverlay;
    public GameObject stageClearPanel;
    public float fadeDuration = 1.5f;

    private bool isClearing = false;

    void Start()
    {
        if (whiteOverlay != null)
            whiteOverlay.color = new Color(1, 1, 1, 0);

        stageClearPanel.SetActive(false);
    }

    public void TriggerStageClear()
    {
        if (!isClearing)
        {
            StartCoroutine(FadeToWhiteAndShowPanel());
        }
    }

    IEnumerator FadeToWhiteAndShowPanel()
    {
        isClearing = true;
        float t = 0f;
        Color start = whiteOverlay.color;
        Color end = new Color(1, 1, 1, 1);

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            whiteOverlay.color = Color.Lerp(start, end, t / fadeDuration);
            yield return null;
        }

        whiteOverlay.color = end;
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
        SceneManager.LoadScene(nextSceneName);
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
