using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public static PlayerStats stats;

    public Player()
    {
        SerializeData serializeData = new SerializeData();
        stats = serializeData.LoadData<PlayerStats>();        
    }    

    public void SavePlayer()
    {
        SerializeData serializeData = new SerializeData();
        serializeData.SaveData<PlayerStats>(stats);
    }
}
