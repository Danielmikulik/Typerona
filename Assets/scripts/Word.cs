using UnityEngine;

[System.Serializable]
public class Word
{
    public string word;
    private int typeIndex;

    private WordDisplay display;

    public Word(string _word, WordDisplay _display)
    {
        this.word = _word;
        this.typeIndex = 0;

        this.display = _display;
        display.SetWord(word);
    }

    public char GetNextLetter()
    {
        return word[typeIndex];
    }

    public void TypeLetter()
    {
        display.ColorLetter(typeIndex++, true);
    }

    public void Misstype()
    {
        display.ColorLetter(typeIndex, false);
    }

    public void Unselect()
    {
        typeIndex = 0;
        display.DecolorWord();
    }

    public bool WordTyped()
    {
        bool wordTyped = (typeIndex >= word.Length);
        if (wordTyped)
        {
            display.RemoveWord();
        }
        return wordTyped;
    }
}
