using System;
using System.Collections.Generic;

[Serializable]
public class LetterTyped
{
    //pressed letter with keypressed time
    public string Letter;
    public string Time;

    public LetterTyped(string time, string letter)
    {
        Letter = letter;
        Time = time;        
    }

}

[Serializable]
public class WordTyped
{
    //typed word with sequence of keypresses with time of each keypress
    public string Word;
    public List<LetterTyped> Sequence;

    public WordTyped(string word, List<LetterTyped> sequence)
    {
        Word = word;
        Sequence = sequence;
    }
}

[Serializable]
public class WordsTypedLog
{
    //list of all words typed with their sequences
    public List<WordTyped> typingSequences;

    public WordsTypedLog()
    {
        typingSequences = new List<WordTyped>();
    }

    public void addSequence(WordTyped wordTyped)
    {
        typingSequences.Add(wordTyped);
    }
}
