using System;
using System.Collections.Generic;

/// <summary>
/// Represents typed letter with the time of keypress.
/// </summary>
[Serializable]
public class LetterTyped
{
    //pressed letter with keyPressed keyPressTime
    public string Letter;
    public string KeyPressTime;

    /// <summary>
    /// Creates new "record" of typed letter with the time of keypress.
    /// </summary>
    /// <param name="keyPressTime">Time of the key press</param>
    /// <param name="letter">Typed letter</param>
    public LetterTyped(string keyPressTime, string letter)
    {
        Letter = letter;
        KeyPressTime = keyPressTime;        
    }

}

/// <summary>
/// Represents successfully typed word with the sequence of keys pressed.
/// </summary>
[Serializable]
public class WordTyped
{
    //typed TypedWord with TypeSequence of keypresses with keyPressTime of each keypress
    public string TypedWord;
    public List<LetterTyped> TypeSequence;

    /// <summary>
    /// Creates new "record" of typed word with the sequence of keys pressed.
    /// </summary>
    /// <param name="typedWord">Typed word</param>
    /// <param name="typeSequence">Sequence of keys pressed</param>
    public WordTyped(string typedWord, List<LetterTyped> typeSequence)
    {
        this.TypedWord = typedWord;
        this.TypeSequence = typeSequence;
    }
}

/// <summary>
/// Collection of all successfully typed words with their type sequences.
/// </summary>
[Serializable]
public class WordsTypedLog
{
    //list of all words typed with their sequences
    public List<WordTyped> TypingSequences;
    
    /// <summary>
    /// Creates new collection of successfully typed words.
    /// </summary>
    public WordsTypedLog()
    {
        TypingSequences = new List<WordTyped>();
    }

    /// <summary>
    /// Adds a new typed word to the collection
    /// </summary>
    /// <param name="wordTyped">Successfully typed word.</param>
    public void AddSequence(WordTyped wordTyped)
    {
        TypingSequences.Add(wordTyped);
    }
}
