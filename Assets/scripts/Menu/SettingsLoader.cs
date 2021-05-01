using UnityEngine;

public class SettingsLoader : MonoBehaviour
{

    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
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

}
