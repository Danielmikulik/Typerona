using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Loading data from server.
/// </summary>
public class HighScoreLoader : MonoBehaviour
{
    [SerializeField] private HighScoreTable table;

    private string URL = "https://localhost:5001/api/Typerona/TopTen";

    /// <summary>
    /// Data loaded from server.
    /// </summary>
    public GameData GameData { get; private set; }

    private void Start()
    {
        GetHighScores();
    }

    /// <summary>
    /// Loading data from server and shows them in table.
    /// </summary>
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

            string json = "{ \"games\" : " + webRequest.downloadHandler.text + "}";   //adding attribute type to downloaded array json for JsonUtility class to deserialize it
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
        GameData = JsonUtility.FromJson<GameData>(json);
        table.ShowHighScores(GameData);
    }    
}
