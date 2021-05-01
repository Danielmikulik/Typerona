using TMPro;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public GameObject uploadErrorText;
    // Start is called before the first frame update
    private void Start()
    {
        transform.Find("valueScore").GetComponent<TextMeshProUGUI>().text = WordManager.Score.ToString();
        transform.Find("valueMistakesCount").GetComponent<TextMeshProUGUI>().text = WordManager.MistakeCount.ToString();
        transform.Find("valueWPM").GetComponent<TextMeshProUGUI>().text = WordManager.WPM.ToString("0.00");
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
