using TMPro;
using UnityEngine;

public class HighScoreTable : MonoBehaviour
{
    public GameObject tableHeader;
    public GameObject loadingError;
    public GameObject loading;

    private HighScoreLoader loader;

    public void OnEnable()
    {
        loader = FindObjectOfType<HighScoreLoader>();
        
        PlayerData highScores = loader.PlayerData;

        if (highScores is null)
        {
            tableHeader.SetActive(false);
            if (!loadingError.activeSelf)
            {
                loading.SetActive(true);
            }            
            return;
        }

        /*//Kontrola, či je aktívny komponent s textom o zlyhaní načítania dát.
        //Môže nastať, ak pri prvom zobrazení dáta neboli načítané, ale pri druhom áno
        if (loadingError.activeSelf)
        {
            tableHeader.SetActive(true);
            loadingError.SetActive(false);
        }*/

        showHighScores(highScores);
    }

    public void showLoading()
    {
        tableHeader.SetActive(false);
        //loadingError.SetActive(false);
        loading.SetActive(true);
    }

    public void showLoadingError()
    {
        //tableHeader.SetActive(false);
        loadingError.SetActive(true);
        loading.SetActive(false);
    }

    public void showHighScores(PlayerData highScores)
    {
        //if (loadingError.activeSelf)
        //{
            tableHeader.SetActive(true);
            //loadingError.SetActive(false);
            loading.SetActive(false);
        //}

        Transform entryContainer = transform.Find("EntryContainer");
        Transform entryTemplate = entryContainer.transform.Find("HighScoreEntry");

        entryTemplate.gameObject.SetActive(false);

        float templateHeight = 28f;
        int index = 0;
        foreach (Player player in highScores.players)
        {
            if (index >= 10)
            {
                break;
            }

            //Debug.Log(player.name);
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * index++);

            entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = index.ToString();
            entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = player.name;
            entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = player.score.ToString();
            entryTransform.Find("mistakesText").GetComponent<TextMeshProUGUI>().text = player.mistakes.ToString();
            entryTransform.Find("WPMText").GetComponent<TextMeshProUGUI>().text = player.WPM.ToString();
            entryTransform.gameObject.SetActive(true);
        }
    }
}
