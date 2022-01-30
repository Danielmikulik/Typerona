using System;
using System.Collections.Generic;

/// <summary>
/// Represents list of game data.
/// </summary>
[System.Serializable]
public class GameData
{
    public List<Game> games;
}

/// <summary>
/// Represents data collected in game.
/// </summary>
[System.Serializable]
public class Game
{
    public string player;
    public int score;
    public int mistakes;
    public float wpm;   //words per minute
    public List<WordTyped> words;

    /// <summary>
    /// Creates new game data storage.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="score"></param>
    /// <param name="mistakes"></param>
    /// <param name="wpm"></param>
    /// <param name="words"></param>
    public Game(string player, int score, int mistakes, float wpm, List<WordTyped> words)
    {
        this.player = player ?? "GUEST";
        this.score = score;
        this.mistakes = mistakes;
        this.wpm = wpm;
        this.words = words;
    }
}
