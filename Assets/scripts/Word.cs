using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Word
{
    private WordDisplay display;

    private DateTime startTime = GameManager.StartTime;
    private string typedWord;
    private List<LetterTyped> lettersTyped = new List<LetterTyped>();
    private int typeIndex;

    public string WordText { get; private set; }    //string to check if the word is typed correctly
    public WordType WordType { get; private set; }

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

    public char GetNextLetter()
    {
        return typeIndex < WordText.Length ? WordText[typeIndex] : char.MinValue;   //next letter that will be typed
    }

    public char GetLastTypedLetter()
    {
        return typeIndex < WordText.Length ? WordText[typeIndex - 1] : char.MinValue;   //last letter that was typed
    }

    public void TypeLetter()
    {       
        if (typeIndex < WordText.Length)
        {
            AddToLog(WordText[typeIndex]);
            typedWord += WordText[typeIndex];
            display.ColorLetter(typeIndex++, LetterState.Correct);            
        }       
    }

    public void MisstypeLetter(char letter)
    {
        AddToLog(letter);
        if (typeIndex < WordText.Length)
        {           
            typedWord += letter;
            display.ColorLetter(typeIndex++, LetterState.Misstyped);
        }
    }

    public void DeleteTypedLetter()
    {
        AddToLog('\b');     //backspace
        if (typeIndex > 0)
        {            
            typedWord = typedWord.Remove(typedWord.Length - 1);                     
            display.ColorLetter(--typeIndex, LetterState.Default, WordType);
        }
    }

    public void Unselect()
    {
        lettersTyped.Clear();       
        typeIndex = 0;
        typedWord = "";        
        display.ColorWord(WordType);
    }

    public bool HasDisplay()
    {
        return display is null;
    }

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
        TimeSpan timeSpan = DateTime.Now.Subtract(startTime);   //time ellapsed from start of the game
        string time = string.Format("{0:D2}:{1:D2}.{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        lettersTyped.Add(new LetterTyped(time, input.ToString()));
    }
}
