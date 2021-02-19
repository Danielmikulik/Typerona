using UnityEngine;
using UnityEngine.UI;

public class WordDisplay : MonoBehaviour
{
    public Text text;

    public void SetWord(string word)
    {
        text.text = word;
        Debug.Log(word + " -> " + text.text);
    }

    public void RemoveLetter() 
    {
        text.text = text.text.Remove(0, 1);
        text.color = Color.red;
    }

    public void RemoveWord()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {        

    }
}
