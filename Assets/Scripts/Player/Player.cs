using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public static PlayerStats stats;
    SerializeData serializeData = new SerializeData();

    public Player()
    {
        stats = serializeData.LoadData<PlayerStats>("playerStats");        
    }    

    public void SavePlayer()
    {
        serializeData.SaveData<PlayerStats>(stats, "playerStats");
    }
}
