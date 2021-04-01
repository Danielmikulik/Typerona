﻿using System;
using System.Collections.Generic;

[System.Serializable]
public class Word
{
    public string word;
    public int typeIndex { get; private set; }  
    private string typedWord;
    private List<LetterTyped> lettersTyped = new List<LetterTyped>();
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

    public char GetLastTypedLetter()
    {
        return typeIndex < word.Length ? word[typeIndex - 1] : char.MinValue;
    }

    public void TypeLetter()
    {       
        if (typeIndex < word.Length)
        {
            lettersTyped.Add(new LetterTyped(DateTime.Now.ToString("HH:mm:ss.ffffff"), word[typeIndex].ToString()));
            this.typedWord += word[typeIndex];
            display.ColorLetter(typeIndex++, LetterState.Correct);            
        }       
    }

    public void MisstypeLetter(char letter)
    {
        lettersTyped.Add(new LetterTyped(DateTime.Now.ToString("HH:mm:ss.ffffff"), letter.ToString()));
        if (typeIndex < word.Length)
        {           
            this.typedWord += letter;
            display.ColorLetter(typeIndex++, LetterState.Misstyped);
        }
    }

    public void DeleteTypedLetter()
    {
        lettersTyped.Add(new LetterTyped(DateTime.Now.ToString("HH:mm:ss.ffffff"), '\b'.ToString()));
        if (typeIndex > 0)
        {            
            typedWord = this.typedWord.Remove(this.typedWord.Length - 1);                     
            display.ColorLetter(--typeIndex, LetterState.Default);
        }
    }

    public void Unselect()
    {
        lettersTyped.Clear();       
        typeIndex = 0;
        typedWord = "";        
        display.DecolorWord();
    }

    public ValueTuple<string, LetterTypedWrapper> WordTyped()
    {        
        bool wordTyped = (typeIndex >= word.Length && typedWord.Equals(word));        
        if (wordTyped)
        {
            display.RemoveWord();
            return (word, new LetterTypedWrapper(lettersTyped));
        }
        return (null, null);
    }
}
