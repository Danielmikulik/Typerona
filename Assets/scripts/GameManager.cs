using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Error message shown after game over in case of unsuccessful post method.
    /// </summary>
    public static bool UploadError { get; private set; }
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
    public void PostStats(Game gameStats) => StartCoroutine(PostData_Coroutine(gameStats));
 
    private IEnumerator PostData_Coroutine(Game gameStats)
    {
        string URL = "https://localhost:5001/api/Typerona";
        string jsonData = JsonUtility.ToJson(gameStats, true);

        Debug.Log(jsonData);
    
        using (UnityWebRequest request = UnityWebRequest.Post(URL, jsonData))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);            
            request.SetRequestHeader("Content-Type", "application/json");
            Debug.Log("Uploading stats...");
            yield return request.SendWebRequest();
            Debug.Log("Data sent");
            if (request.isNetworkError || request.isHttpError)
            {
                UploadError = true;
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
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
