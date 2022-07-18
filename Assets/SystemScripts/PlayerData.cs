using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public int highscore;

    public PlayerData(MainManager data)
    {
        this.name = data.playerName;
        this.highscore = data.highscore;
    }
    
    //when game is run for the first time on a new device - before we enter player name, our first player is "Player"
    public PlayerData()
    {
        name = "Player";
        highscore = 0;
    }
}
