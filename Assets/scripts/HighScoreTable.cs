using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreTable : MonoBehaviour
{
    public GameObject tableHeader;
    public GameObject loadingError;
   
    private HighScoreLoader loader;

    public void OnEnable()
    {
        loader = FindObjectOfType<HighScoreLoader>();
        Transform entryContainer = transform.Find("EntryContainer");
        Transform entryTemplate = entryContainer.transform.Find("HighScoreEntry");

        entryTemplate.gameObject.SetActive(false);

        PlayerData highScores = loader.PlayerData;

        if (highScores is null)
        {
            tableHeader.SetActive(false);
            loadingError.SetActive(true);
            return;
        }

        if (loadingError.activeSelf)
        {
            tableHeader.SetActive(true);
            loadingError.SetActive(false);
        }

        float templateHeight = 30f;
        int index = 0;
        foreach (Player player in highScores.players)
        {
            Debug.Log(player.name);
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
