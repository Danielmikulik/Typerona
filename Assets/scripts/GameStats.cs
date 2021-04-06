﻿using TMPro;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("valueScore").GetComponent<TextMeshProUGUI>().text = WordManager.Score.ToString();
        transform.Find("valueMistakesCount").GetComponent<TextMeshProUGUI>().text = WordManager.MistakeCount.ToString();
        transform.Find("valueWPM").GetComponent<TextMeshProUGUI>().text = WordManager.WPM.ToString("0.00");
    }
}
