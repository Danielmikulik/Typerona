using System;
using System.Net.Http;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Error message shown after game over in case of unsuccessful post method.
    /// </summary>
    public static bool UploadError { get; set; }
    /// <summary>
    /// Time of the start of the game.
    /// </summary>
    public static DateTime StartTime { get; private set; }


    [SerializeField] private GameObject gameOver;
    [SerializeField] private WordManager wordManager;

    private bool gameEnded = false;
    private float musicVolume;
    private const float dampenedMusicVolume = 0.08f;
    private AudioManager audioManager;

    private void Start()
    {
        StartTime = DateTime.Now;
        audioManager = AudioManager.Instance;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Shows game over panel, decreases music volume, plays sound and then increases music volume back.
    /// </summary>
    public void EndGame()
    {
        if (!gameEnded)
        {
            musicVolume = PlayerPrefs.HasKey("musicVolume") ? PlayerPrefs.GetFloat("musicVolume") : 0.25f;
            float SFXVolume = PlayerPrefs.HasKey("SFXVolume") ? PlayerPrefs.GetFloat("SFXVolume") : 1;
            if (musicVolume > dampenedMusicVolume && SFXVolume > dampenedMusicVolume)
            {
                audioManager.ChangeMusicVolume(dampenedMusicVolume); //dampen music volume, to not interfere with GameOver sound
            }

            audioManager.Play("GameOver");

            gameEnded = true;
            gameOver.SetActive(true);

            wordManager.WriteStats();

            //destroy all viruses on the scene
            GameObject[] viruses = GameObject.FindGameObjectsWithTag("Virus");
            foreach (GameObject virus in viruses)
            {
                virus.GetComponent<VirusMovement>().enabled = false;
            }

            Invoke(nameof(LoadGameOverScene), 5);
            Invoke(nameof(RevertMusicVolume), 4);
        }       
    }

    /// <summary>
    /// Calls Post method on REST API to send game data to server.
    /// </summary>
    /// <param name="gameStats"></param>
    public async void PostData(Game gameStats)
    {
        string URL = "https://localhost:5001/api/Typerona";
        string jsonData = JsonUtility.ToJson(gameStats, true);

        Debug.Log(jsonData);

        using (HttpClient request = new HttpClient())
        {
            try
            {
                var response = await request.PostAsync(URL, new StringContent(jsonData, Encoding.UTF8, "application/json"));
                var content = await response.Content.ReadAsStringAsync();
                Debug.Log("Data sent");
                Debug.Log(content);
            }
            catch (HttpRequestException e)
            {
                UploadError = true;
                Debug.Log(e);
            }
        }
    }

    private void RevertMusicVolume()
    {
        audioManager.IncreaseMusicVolumeGradually(musicVolume);
    }

    private void LoadGameOverScene()
    {       
        SceneManager.LoadScene("GameOver");
    }
}
