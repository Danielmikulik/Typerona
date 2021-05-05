using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class HighScoreLoader : MonoBehaviour
{
    [SerializeField] private HighScoreTable table;

    private string URL = "http://localhost/api/players";

    public PlayerData PlayerData { get; private set; }

    private void Start()
    {
        GetHighScores();
    }

    public void GetHighScores()
    {
        StartCoroutine(GetData());
    }

    private IEnumerator GetData()
    {
        Debug.Log("Processing data, Please wait...");

        using (UnityWebRequest webRequest = UnityWebRequest.Get(URL))
        {
            table.ShowLoading();
            yield return webRequest.SendWebRequest();

            string json = "{ \"players\" : " + webRequest.downloadHandler.text + "}";   //adding attribute type to downloaded array json for JsonUtility class to deserialize it
            if (webRequest.isHttpError || webRequest.isNetworkError)
            {
                Debug.Log(webRequest.error);
                table.ShowLoadingError();
            }
            else
            {
                Debug.Log(json);
                ProcessData(json);                
            }
        }  
    }

    private void ProcessData(string json) 
    {
        PlayerData = JsonUtility.FromJson<PlayerData>(json);
        table.ShowHighScores(PlayerData);
    }    
}
