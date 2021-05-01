using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider SFXVolumeSlider;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public Toggle fullscreen;
    public AudioManager audioManager;

    private Resolution[] resolutions;

    private void Start()
    {
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));
        //Debug.Log(PlayerPrefs.GetInt("quality"));
        qualityDropdown.value = PlayerPrefs.HasKey("quality") ? PlayerPrefs.GetInt("quality") : QualitySettings.GetQualityLevel();       

        fullscreen.isOn = (PlayerPrefs.GetInt("fullscreen") == 1);
        //Debug.Log(PlayerPrefs.GetInt("fullscreen"));
        Screen.fullScreen = fullscreen.isOn;
        
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

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
            SetMusicVolume(musicVolumeSlider.value);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
            SetSFXVolume(SFXVolumeSlider.value);
        }
    }

    private void OnEnable()
    {
        musicVolumeSlider.Select();
    }

    public void SetMusicVolume(float volume)
    {
        audioManager.ChangeMusicVolume(volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioManager.ChangeSFXVolume(volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("quality", qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("fullscreen", isFullscreen ? 1 : 0);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        PlayerPrefs.SetInt("resolution", resolutionIndex);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
