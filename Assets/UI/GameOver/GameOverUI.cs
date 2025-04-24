// เผื่อจะใช้เรียก script ตอน player ตาย FindObjectOfType<GameOverUI>().TriggerGameOver();

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header("UI References")]
    public Image fadeOverlay;
    public GameObject gameOverPanel;
    public float fadeDuration = 1.5f;

    private bool isFading = false;

    void Start()
    {
        fadeOverlay.color = new Color(0, 0, 0, 0);
        gameOverPanel.SetActive(false);
    }

    // Call this when the player dies
    public void TriggerGameOver()
    {
        if (!isFading)
        {
            StartCoroutine(FadeToBlack());
        }
    }

    IEnumerator FadeToBlack()
    {
        isFading = true;

        float t = 0f;
        Color startColor = fadeOverlay.color;
        Color endColor = new Color(0, 0, 0, 1);

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            fadeOverlay.color = Color.Lerp(startColor, endColor, t / fadeDuration);
            yield return null;
        }

        fadeOverlay.color = endColor;
        gameOverPanel.SetActive(true);
    }

    // Hook these up to retry/quit buttons
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
