using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Managing of options in the options panel.
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullscreen;
    private AudioManager audioManager;

    private Resolution[] resolutions;

    private void Start()
    {
        audioManager = AudioManager.Instance; 

        //making UI match the last used settings
        qualityDropdown.value = PlayerPrefs.HasKey("quality") ? PlayerPrefs.GetInt("quality") : QualitySettings.GetQualityLevel();       

        fullscreen.isOn = (PlayerPrefs.GetInt("fullscreen") == 1);
        Screen.fullScreen = fullscreen.isOn;
        
        resolutions = Screen.resolutions;

        List<string> options = new List<string>();

        //filling the resolution dropdown with device supported resolutions and refresh rates
        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @" + resolutions[i].refreshRate + "Hz";           
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        currentResIndex = PlayerPrefs.HasKey("resolution") ? PlayerPrefs.GetInt("resolution") : currentResIndex;

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        Resolution resolution = resolutions[currentResIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        resolutionDropdown.RefreshShownValue();

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

    private void OnEnable()
    {
        musicVolumeSlider.Select();
    }

    /// <summary>
    /// Change of music volume. Method is called when music volume slider value is changed.
    /// </summary>
    /// <param name="volume">Value from slider</param>
    public void SetMusicVolume(float volume)
    {
        audioManager.ChangeMusicVolume(volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    /// <summary>
    /// Change of SFX volume. Method is called when SFX volume slider value is changed.
    /// </summary>
    /// <param name="volume">Value from slider</param>
    public void SetSFXVolume(float volume)
    {
        audioManager.ChangeSFXVolume(volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    /// <summary>
    /// Change of quality settings. Method is called when quality settings are changed.
    /// </summary>
    /// <param name="qualityIndex">Index of quality settings in dropdown menu.</param>
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("quality", qualityIndex);
    }

    /// <summary>
    /// Change of fullscreen mode. Method is called when fullscreen mode is changed.
    /// </summary>
    /// <param name="isFullscreen">boolean value of fullscreen toggle.</param>
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("fullscreen", isFullscreen ? 1 : 0);
    }

    /// <summary>
    /// Change of screen resolution. Method is called when resolution is changed.
    /// </summary>
    /// <param name="resolutionIndex">Index of resolution in dropdown menu.</param>
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        PlayerPrefs.SetInt("resolution", resolutionIndex);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
