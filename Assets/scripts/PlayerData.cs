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
    //public int id;
    public string name;
    public int score;
    public int mistakes;
    public float WPM;
    //public DateTime? created_at { get; set; }
    //public DateTime? updated_at { get; set; }
}
