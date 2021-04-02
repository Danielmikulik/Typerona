using System;
using System.Collections.Generic;

[Serializable]
public class LetterTyped
{
    public string Letter;
    public string Time;

    public LetterTyped(string time, string letter)
    {
        this.Letter = letter;
        this.Time = time;        
    }

}

[Serializable]
public class WordTyped
{
    public string Word;
    public List<LetterTyped> Sequence;

    public WordTyped(string word, List<LetterTyped> sequence)
    {
        this.Word = word;
        this.Sequence = sequence;
    }
}

[Serializable]
public class WordsTypedLog
{
    public List<WordTyped> typingSequences;

    public WordsTypedLog()
    {
        this.typingSequences = new List<WordTyped>();
    }

    public void addSequence(WordTyped wordTyped)
    {
        this.typingSequences.Add(wordTyped);
    }
}
