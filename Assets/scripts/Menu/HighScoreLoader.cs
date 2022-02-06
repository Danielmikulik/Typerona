using System.Net.Http;
using UnityEngine;

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
    public async void GetHighScores()
    {
        Debug.Log("Processing data, Please wait...");

        using (HttpClient client = new HttpClient())
        {
            table.ShowLoading();
            try
            {
                var response = await client.GetAsync(URL);
                var content = await response.Content.ReadAsStringAsync();

                string json = "{ \"games\" : " + content + "}";   //adding attribute type to downloaded array json for JsonUtility class to deserialize it
                Debug.Log(json);
                ProcessData(json);
            }
            catch (HttpRequestException e)
            {
                Debug.Log(e);
                table.ShowLoadingError();
            }
        }
    }

    private void ProcessData(string json)
    {
        GameData = JsonUtility.FromJson<GameData>(json);
        table.ShowHighScores(GameData);
    }
}