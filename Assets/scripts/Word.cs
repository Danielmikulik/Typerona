using System;
using System.Collections.Generic;

[System.Serializable]
public class Word
{
    private DateTime startTime = GameManager.StartTime;
    public string word;
    public int typeIndex { get; private set; }  
    private string typedWord;
    private List<LetterTyped> lettersTyped = new List<LetterTyped>();
    public WordType WordType { get; private set; }

    private WordDisplay display;

    public Word(string _word, WordDisplay _display, WordType _wordType)
    {
        this.word = _word;
        this.typeIndex = 0;

        this.display = _display;
        this.display.SetWord(word);
        this.WordType = _wordType;

        if (this.WordType != WordType.Normal)
        {
            this.display.ColorWord(this.WordType);
        }
    }

    public char GetNextLetter()
    {
        return typeIndex < word.Length ? word[typeIndex] : char.MinValue;
    }

    public char GetLastTypedLetter()
    {
        return typeIndex < word.Length ? word[typeIndex - 1] : char.MinValue;
    }

    public void TypeLetter()
    {       
        if (typeIndex < word.Length)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(startTime);
            string time = string.Format("{0:D2}:{1:D2}.{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
            lettersTyped.Add(new LetterTyped(time, word[typeIndex].ToString()));
            this.typedWord += word[typeIndex];
            display.ColorLetter(typeIndex++, LetterState.Correct);            
        }       
    }

    public void MisstypeLetter(char letter)
    {
        TimeSpan timeSpan = DateTime.Now.Subtract(startTime);
        string time = string.Format("{0:D2}:{1:D2}.{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        lettersTyped.Add(new LetterTyped(time, letter.ToString()));
        if (typeIndex < word.Length)
        {           
            this.typedWord += letter;
            display.ColorLetter(typeIndex++, LetterState.Misstyped);
        }
    }

    public void DeleteTypedLetter()
    {
        TimeSpan timeSpan = DateTime.Now.Subtract(startTime);
        string time = string.Format("{0:D2}:{1:D2}.{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        lettersTyped.Add(new LetterTyped(time, '\b'.ToString()));
        if (typeIndex > 0)
        {            
            typedWord = this.typedWord.Remove(this.typedWord.Length - 1);                     
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

    public bool hasDisplay()
    {
        return display is null;
    }

    public WordTyped WordTyped()
    {        
        bool wordTyped = (typedWord.Equals(word));        
        if (wordTyped)
        {
            display.RemoveWord();
            return new WordTyped(word, lettersTyped);
        }
        return null;
    }

    private void AddToLog(char input)
    {
        //DateTime time = DateTime.Now - startTime;
        //lettersTyped.Add(new LetterTyped(DateTime.Now.ToString("HH:mm:ss.ffffff"), letter.ToString()));
    }
}
