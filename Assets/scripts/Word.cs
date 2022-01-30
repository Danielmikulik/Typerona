using System;
using System.Collections.Generic;

/// <summary>
/// Single word that needs to be typed.
/// </summary>
[System.Serializable]
public class Word
{
    private WordDisplay display;

    private DateTime startTime = GameManager.StartTime;
    private string typedWord;
    private List<LetterTyped> lettersTyped = new List<LetterTyped>();
    private int typeIndex;

    /// <summary>
    /// text of the word to check if the word is typed correctly
    /// </summary>
    public string WordText { get; private set; }
    
    /// <summary>
    /// Type (effect) of the word.
    /// </summary>
    public WordType WordType { get; private set; }

    /// <summary>
    /// New instance of the word to be typed.
    /// </summary>
    /// <param name="word">word text</param>
    /// <param name="_display">GUI of the word</param>
    /// <param name="_wordType">Type of the word</param>
    public Word(string word, WordDisplay _display, WordType _wordType)
    {
        WordText = word;
        typeIndex = 0;

        display = _display;
        display.SetWord(WordText);
        WordType = _wordType;

        if (WordType != WordType.Normal)
        {
            display.ColorWord(WordType);
        }
    }

    /// <summary>
    /// Next letter to by typed.
    /// </summary>
    /// <returns>Next letter to by typed</returns>
    public char GetNextLetter()
    {
        return typeIndex < WordText.Length ? WordText[typeIndex] : char.MinValue;   //next letter that will be typed
    }

    /// <summary>
    /// A letter which was typed last.
    /// </summary>
    /// <returns>A letter which was typed last</returns>
    public char GetLastTypedLetter()
    {
        return typeIndex < WordText.Length ? WordText[typeIndex - 1] : char.MinValue;   //last letter that was typed
    }

    /// <summary>
    /// Marks the letter as typed and changes it's color on gui to green.
    /// </summary>
    public void TypeLetter()
    {       
        if (typeIndex < WordText.Length)
        {
            AddToLog(WordText[typeIndex]);
            typedWord += WordText[typeIndex];
            display.ColorLetter(typeIndex++, LetterState.Correct);            
        }       
    }

    /// <summary>
    /// Marks the letter as missTyped and changes it's color on gui to red.
    /// </summary>
    /// <param name="letter">MissTyped letter</param>
    public void MisstypeLetter(char letter)
    {
        AddToLog(letter);
        if (typeIndex < WordText.Length)
        {
            typedWord += letter;
            display.ColorLetter(typeIndex++, LetterState.MissTyped);
        }
    }

    /// <summary>
    /// Marks last typed letter as untyped and changes it's color on gui to default.
    /// </summary>
    public void DeleteTypedLetter()
    {
        AddToLog('\b');     //backspace
        if (typeIndex > 0)
        {            
            typedWord = typedWord.Remove(typedWord.Length - 1);                     
            display.ColorLetter(--typeIndex, LetterState.Default, WordType);
        }
    }

    /// <summary>
    /// Unselect the selected (locked) word
    /// </summary>
    public void Unselect()
    {
        lettersTyped.Clear();       
        typeIndex = 0;
        typedWord = "";        
        display.ColorWord(WordType);
    }

    /// <summary>
    /// Checks if word has display (gui).
    /// </summary>
    /// <returns>word has display (gui)</returns>
    public bool HasDisplay()
    {
        return display is null;
    }

    /// <summary>
    /// Checks if word is typed and removes it, if it is.
    /// </summary>
    /// <returns>Typed word</returns>
    public WordTyped WordTyped()
    {        
        bool wordTyped = (typedWord.Equals(WordText));        
        if (wordTyped)
        {
            display.RemoveWord();
            return new WordTyped(WordText, lettersTyped);
        }
        return null;
    }

    private void AddToLog(char input)
    {
        TimeSpan timeSpan = DateTime.Now.Subtract(startTime);   //time elapsed from start of the game
        string time = string.Format("{0:D2}:{1:D2}.{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        lettersTyped.Add(new LetterTyped(time, input.ToString()));
    }
}
