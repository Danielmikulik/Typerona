using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Word
{
    public string word;
    private int typeIndex;
    private List<int> missTypedIndexes = new List<int>();

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
        if (typeIndex < word.Length)
        {
            display.ColorLetter(typeIndex++, LetterState.Correct);
        }       
    }

    public void MisstypeLetter()
    {
        if (typeIndex < word.Length)
        {
            missTypedIndexes.Add(typeIndex);
            display.ColorLetter(typeIndex++, LetterState.Misstyped);
        }
    }

    public void DeleteTypedLetter()
    {
        if (typeIndex > 0)
        {
            if (missTypedIndexes[missTypedIndexes.Count() - 1] == typeIndex - 1)
            {
                missTypedIndexes.RemoveAt(missTypedIndexes.Count() - 1);
            }
            display.ColorLetter(--typeIndex, LetterState.Default);
        }
    }

    public void Unselect()
    {
        typeIndex = 0;
        missTypedIndexes.Clear();
        display.DecolorWord();
    }

    public bool WordTyped()
    {
        bool wordTyped = (typeIndex >= word.Length && missTypedIndexes.Count <= 0);
        if (wordTyped)
        {
            display.RemoveWord();
        }
        return wordTyped;
    }
}
