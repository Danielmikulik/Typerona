using TMPro;
using UnityEngine;

public class HighScoreTable : MonoBehaviour
{
    [SerializeField] private GameObject tableHeader;
    [SerializeField] private GameObject loadingError;   //Text saying there's been an error
    [SerializeField] private GameObject loading;    //Text saying that data is being downloaded

    private HighScoreLoader loader;

    public void OnEnable()
    {
        loader = FindObjectOfType<HighScoreLoader>();
        
        PlayerData highScores = loader.PlayerData;

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

    public void ShowLoading()
    {
        tableHeader.SetActive(false);
        loading.SetActive(true);
    }

    public void ShowLoadingError()
    {
        loadingError.SetActive(true);
        loading.SetActive(false);
    }

    public void ShowHighScores(PlayerData highScores)
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
        foreach (Player player in highScores.players)
        {
            if (index >= 10)
            {
                break;
            }

            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * index++);    //moving the entry transform down

            entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = index.ToString();
            entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = player.name;
            entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = player.score.ToString();
            entryTransform.Find("mistakesText").GetComponent<TextMeshProUGUI>().text = player.mistakes.ToString();
            entryTransform.Find("WPMText").GetComponent<TextMeshProUGUI>().text = player.WPM.ToString();
            entryTransform.gameObject.SetActive(true);
        }
    }
}
