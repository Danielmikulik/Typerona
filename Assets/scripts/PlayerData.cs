using System;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public List<Player> players;
}

[System.Serializable]
public class Player
{
    public string name;
    public int score;
    public int mistakes;
    public float WPM;   //words per minute
    public WordsTypedLog wordSequences;

    public Player(string name, int score, int mistakes, float WPM, WordsTypedLog wordSequences)
    {
        this.name = name != null ? name : "GUEST";
        this.score = score;
        this.mistakes = mistakes;
        this.WPM = WPM;
        this.wordSequences = wordSequences;
    }
}
