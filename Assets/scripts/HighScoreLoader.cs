using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HighScoreLoader : MonoBehaviour
{
    private string URL = "http://localhost/api/players";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getData());
    }

    IEnumerator getData()
    {
        Debug.Log("Processing data, Please wait...");

        WWW www = new WWW(URL);        
        yield return www;
        string json = "{ \"players\" : " + www.text + "}";
        if (www.error == null)
        {
            Debug.Log(json);
            processData(json);
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    public void processData(string json) 
    {
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        foreach (Player player in data.players)
        {
            Debug.Log(player.name + " " + player.score + " " + player.mistakes + " " + player.WPM);
        }
    }
}
