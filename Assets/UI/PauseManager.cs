using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public Button pauseButton;
    public Image pauseButtonImage; // <- Reference to the Button’s Image component
    public Sprite pauseSprite;
    public Sprite playSprite;
    public Slider volumeSlider;

    private bool isPaused = false;

    void Start()
    {
        pauseMenuPanel.SetActive(false);
        pauseButton.onClick.AddListener(TogglePause);

        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.onValueChanged.AddListener(SetVolume);
        AudioListener.volume = volumeSlider.value;

        pauseButtonImage.sprite = pauseSprite; // start with pause icon
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuPanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;

        // Swap the icon
        pauseButtonImage.sprite = isPaused ? playSprite : pauseSprite;
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
