using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [Header("Water Bubbles")]
    public List<Image> bubbles;         // Drag your 3 bubble images here
    public int maxAir = 3;

    [Header("Pause Menu")]
    public GameObject pauseMenuPanel;   // Your pause panel
    public Image pauseButtonImage;      // Image component on the pause button
    public Sprite pauseSprite;          // Icon to show when NOT paused
    public Sprite continueSprite;       // Icon to show when paused

    [Header("Settings")]
    public Slider volumeSlider;         // Drag your volume slider here

    private bool isPaused = false;

    void Start()
    {
        // Restore saved volume, or use Inspector value if none
        if (PlayerPrefs.HasKey("Volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume");
            volumeSlider.value = savedVolume;
            AudioListener.volume = savedVolume;
        }
        else
        {
            SetVolume(volumeSlider.value); // Use default from Inspector (e.g., 0.8)
        }

        volumeSlider.onValueChanged.AddListener(SetVolume);

        pauseMenuPanel.SetActive(false);
        pauseButtonImage.sprite = pauseSprite;

        UpdateBubbles(maxAir);
    }

    // Called from pause button
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseMenuPanel.SetActive(isPaused);
        pauseButtonImage.sprite = isPaused ? continueSprite : pauseSprite;
    }

    // Called from quit button
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Call this externally (like from player health/drowning script)
    public void UpdateBubbles(int currentAir)
    {
        int total = bubbles.Count;
        for (int i = 0; i < total; i++)
        {
            bubbles[i].enabled = i >= total - currentAir;
        }
    }

    // Called automatically from volume slider
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
