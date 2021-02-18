using UnityEngine;
using UnityEngine.UI;

public class WordDisplay : MonoBehaviour
{
    public TextMesh text;

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
        DestroyImmediate(gameObject, true);
    }

    // Update is called once per frame
    void Update()
    {        

    }
}
