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
    /// <param name="player">Player name</param>
    /// <param name="score">Reached score</param>
    /// <param name="mistakes">Count of mistakes</param>
    /// <param name="wpm">Typing speed (Words Per Minute)</param>
    /// <param name="words">Typed words</param>
    public Game(string player, int score, int mistakes, float wpm, List<WordTyped> words)
    {
        this.player = player ?? "GUEST";
        this.score = score;
        this.mistakes = mistakes;
        this.wpm = wpm;
        this.words = words;
    }
}
