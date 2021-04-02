using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject GameOver;
    public WordManager wordManager;

    bool gameEnded = false;
    public void EndGame()
    {
        if (!gameEnded)
        {           
            gameEnded = true;
            GameOver.SetActive(true);
            wordManager.writeStats();
            Invoke("Restart", 4);
        }       
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PostStats(Player playerStats) => StartCoroutine(PostData_Coroutine(playerStats));

    private IEnumerator PostData_Coroutine(Player playerStats)
    {
        string URL = "http://localhost/api/players";

        //Player player = new Player();
        //player.name = name;
        //player.score = score;
        //player.mistakes = mistakes;
        //player.WPM = WPM;
        string jsonData = JsonUtility.ToJson(playerStats, true);

        Debug.Log(jsonData);
    
        using (UnityWebRequest request = UnityWebRequest.Post(URL, jsonData))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            //request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            Debug.Log("Uploading stats...");
            yield return request.SendWebRequest();
            Debug.Log("Data sent");
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("paek");
                Debug.Log(request.downloadHandler.text);
            }
        }
    }
}
