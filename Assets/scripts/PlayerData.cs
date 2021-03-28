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
    public float WPM;
}
