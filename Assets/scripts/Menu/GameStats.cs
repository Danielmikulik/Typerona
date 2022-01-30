using TMPro;
using UnityEngine;

/// <summary>
/// Shows game stats at the end of a game.
/// </summary>
public class GameStats : MonoBehaviour
{
    [SerializeField] private GameObject uploadErrorText;

    private void Start()
    {
        //show stats on GameOver scene
        transform.Find("valueScore").GetComponent<TextMeshProUGUI>().text = WordManager.Score.ToString();
        transform.Find("valueMistakesCount").GetComponent<TextMeshProUGUI>().text = WordManager.MistakeCount.ToString();
        transform.Find("valueWPM").GetComponent<TextMeshProUGUI>().text = WordManager.WPM.ToString("0.00");
        
        //if data failed to be sent to server, error text is shown
        if (GameManager.UploadError)
        {
            uploadErrorText.SetActive(true);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
}
