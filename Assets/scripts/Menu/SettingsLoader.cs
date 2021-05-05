using UnityEngine;

public class SettingsLoader : MonoBehaviour
{

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.Instance;

        //setting game to last used settings
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("quality"));
        Screen.fullScreen = (PlayerPrefs.GetInt("fullscreen") == 1);
        if (PlayerPrefs.HasKey("resolution"))
        {
            int currentResIndex = PlayerPrefs.GetInt("resolution");
            Resolution resolution = Screen.resolutions[currentResIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            audioManager.ChangeMusicVolume(PlayerPrefs.GetFloat("musicVolume"));
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            audioManager.ChangeSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
