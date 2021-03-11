using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Word
{
    public string word;
    private int typeIndex;
    private List<int> misstypedIndexes = new List<int>();
    public WordType wordType { get; private set; }

    private WordDisplay display;

    public Word(string _word, WordDisplay _display, WordType _wordType)
    {
        this.word = _word;
        this.typeIndex = 0;

        this.display = _display;
        this.display.SetWord(word);
        this.wordType = _wordType;

        if (this.wordType != WordType.Normal)
        {
            this.display.ColorWord(this.wordType);
        }
    }

    public char GetNextLetter()
    {
        return typeIndex < word.Length ? word[typeIndex] : char.MinValue;
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
            misstypedIndexes.Add(typeIndex);
            display.ColorLetter(typeIndex++, LetterState.Misstyped);
        }
    }

    public void DeleteTypedLetter()
    {
        if (typeIndex > 0)
        {
            if (misstypedIndexes[misstypedIndexes.Count() - 1] == typeIndex - 1)
            {
                misstypedIndexes.RemoveAt(misstypedIndexes.Count() - 1);
            }
            display.ColorLetter(--typeIndex, LetterState.Default);
        }
    }

    public void Unselect()
    {
        typeIndex = 0;
        misstypedIndexes.Clear();
        display.DecolorWord();
    }

    public bool WordTyped()
    {
        bool wordTyped = (typeIndex >= word.Length && misstypedIndexes.Count <= 0);
        if (wordTyped)
        {
            display.RemoveWord();
        }
        return wordTyped;
    }
}
