using TMPro;
using UnityEngine;

/// <summary>
/// Shows table of top 10 players.
/// </summary>
public class HighScoreTable : MonoBehaviour
{
    [SerializeField] private GameObject tableHeader;
    [SerializeField] private GameObject loadingError;   //Text saying there's been an error
    [SerializeField] private GameObject loading;    //Text saying that data is being downloaded

    private HighScoreLoader loader;

    private void OnEnable()
    {
        loader = FindObjectOfType<HighScoreLoader>();
        
        GameData highScores = loader.GameData;

        if (highScores is null) //data not loaded
        {
            tableHeader.SetActive(false);
            if (!loadingError.activeSelf)   //there's no error yet, so the data may be loading
            {
                loading.SetActive(true);
            }            
            return;
        }

        ShowHighScores(highScores);
    }

    /// <summary>
    /// shows loading data message.
    /// </summary>
    public void ShowLoading()
    {
        tableHeader.SetActive(false);
        loading.SetActive(true);
    }

    /// <summary>
    /// Shows loading error message.
    /// </summary>
    public void ShowLoadingError()
    {
        loadingError.SetActive(true);
        loading.SetActive(false);
    }

    /// <summary>
    /// Shows a table of top 10 players. If data can't be loaded, error message is shown.
    /// </summary>
    /// <param name="highScores">top 10 players</param>
    public void ShowHighScores(GameData highScores)
    {
        if (loading.activeSelf)
        {
            tableHeader.SetActive(true);
            loading.SetActive(false);
        }

        Transform entryContainer = transform.Find("EntryContainer");    //container for high score entries
        Transform entryTemplate = entryContainer.transform.Find("HighScoreEntry");   //template for high score entries

        if (entryContainer.childCount > 1)  //table already contains data;
        {
            return;
        }

        entryTemplate.gameObject.SetActive(false);

        float templateHeight = 28f;     //vertical space between entries
        int index = 0;
        foreach (Game player in highScores.games)
        {
            if (index >= 10)
            {
                break;
            }

            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * index++);    //moving the entry transform down

            entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = index.ToString();
            entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = player.player;
            entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = player.score.ToString();
            entryTransform.Find("mistakesText").GetComponent<TextMeshProUGUI>().text = player.mistakes.ToString();
            entryTransform.Find("WPMText").GetComponent<TextMeshProUGUI>().text = player.wpm.ToString();
            entryTransform.gameObject.SetActive(true);
        }
    }
}
