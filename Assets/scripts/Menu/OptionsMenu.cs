using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Slider slider;
    public TMP_Dropdown resolutionDropdown;
    public AudioManager audioManager;

    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void OnEnable()
    {
        slider.Select();
    }

    public void SetMusicVolume(float volume)
    {
        audioManager.ChangeMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
       audioManager.ChangeSFXVolume(volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
