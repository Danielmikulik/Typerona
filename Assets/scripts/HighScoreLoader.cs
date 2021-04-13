using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class HighScoreLoader : MonoBehaviour
{
    private string URL = "http://localhost/api/players";
    private PlayerData playerData;

    public PlayerData PlayerData { get; private set; }

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(getData());
    }

    IEnumerator getData()
    {
        Debug.Log("Processing data, Please wait...");

        using (UnityWebRequest webRequest = UnityWebRequest.Get(URL))
        {
            yield return webRequest.SendWebRequest();

            string json = "{ \"players\" : " + webRequest.downloadHandler.text + "}";
            if (webRequest.isHttpError || webRequest.isNetworkError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                Debug.Log(json);
                processData(json);                
            }
        }  
    }

    public void processData(string json) 
    {
        PlayerData = JsonUtility.FromJson<PlayerData>(json);
        //foreach (Player player in PlayerData.players)
        //{
        //    Debug.Log(player.name + " " + player.score + " " + player.mistakes + " " + player.WPM);
        //}
    }    
}
