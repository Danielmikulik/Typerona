using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Ak nastane chyba pri odosielaní dát na konci hry
    public static bool UploadError { get; private set; }
    public static DateTime StartTime { get; private set; }

    private static bool uploadError;
    private static DateTime startTime;
 
    public GameObject GameOver;
    public WordManager wordManager;

    private bool gameEnded = false;

    private void Start()
    {
        StartTime = DateTime.Now;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

    public void EndGame()
    {
        if (!gameEnded)
        {
            AudioManager audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            /*float musicVolume = PlayerPrefs.GetFloat("musicVolume");
            if (musicVolume > 0.1f)
            {
                Debug.Log("menim " + musicVolume);
                audioManager.ChangeMusicVolume(0.1f);
            }*/
            audioManager.Play("GameOver");

            gameEnded = true;
            GameOver.SetActive(true);

            wordManager.writeStats();

            GameObject[] viruses = GameObject.FindGameObjectsWithTag("Virus");
            foreach (GameObject virus in viruses)
            {
                virus.GetComponent<VirusMovement>().enabled = false;
            }

            Invoke("LoadGameOverScene", 5);

            /*if (musicVolume > 0.1)
            {
                audioManager.ChangeMusicVolume(musicVolume);
            }  */         
        }       
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void PostStats(Player playerStats) => StartCoroutine(PostData_Coroutine(playerStats));

    private IEnumerator PostData_Coroutine(Player playerStats)
    {
        string URL = "http://localhost/api/players";
        string jsonData = JsonUtility.ToJson(playerStats, true);

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
}
