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
public class LetterTypedWrapper
{
    public List<LetterTyped> wordTyped;

    public LetterTypedWrapper(List<LetterTyped> wordTyped)
    {
        this.wordTyped = wordTyped;
    }
}

[Serializable]
public class WordsTypedLog
{
    public List<(string, LetterTypedWrapper)> log;

    public WordsTypedLog(List<(string, LetterTypedWrapper)> log)
    {
        this.log = log;
    }
}
