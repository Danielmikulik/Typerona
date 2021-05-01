using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class HighScoreLoader : MonoBehaviour
{
    public HighScoreTable table;

    private string URL = "http://localhost/api/players";
    private PlayerData playerData;

    public PlayerData PlayerData { get; private set; }

    // Start is called before the first frame update
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
            table.showLoading();
            yield return webRequest.SendWebRequest();

            string json = "{ \"players\" : " + webRequest.downloadHandler.text + "}";
            if (webRequest.isHttpError || webRequest.isNetworkError)
            {
                Debug.Log(webRequest.error);
                table.showLoadingError();
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
        table.showHighScores(PlayerData);
    }    
}
