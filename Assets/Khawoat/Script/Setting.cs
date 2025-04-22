using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    [Header("Display")]
    public Toggle fullscreenToggle;

    [Header("Control Mode")]
    public Toggle useJoystickToggle;

    private void Start()
    {
        LoadSettings();
    }

    public void SetVolume(float value)
    {
        float dB = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20;
        audioMixer.SetFloat("MasterVolume", dB);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetControlMode(bool useJoystick)
    {
        PlayerPrefs.SetInt("UseJoystick", useJoystick ? 1 : 0);
    }

    public void ResetSettings()
    {
        PlayerPrefs.DeleteAll();
        LoadSettings();
    }

    private void LoadSettings()
    {
        // Volume
        float vol = PlayerPrefs.GetFloat("MasterVolume", 1f);
        volumeSlider.value = vol;
        SetVolume(vol);

        // Fullscreen
        bool isFull = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        fullscreenToggle.isOn = isFull;
        SetFullscreen(isFull);

        // Control Mode
        bool joystick = PlayerPrefs.GetInt("UseJoystick", 0) == 1;
        useJoystickToggle.isOn = joystick;
        SetControlMode(joystick);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("MasterVolume", volumeSlider.value);
        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
    }
}
