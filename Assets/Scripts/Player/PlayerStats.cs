using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    // player stats
    public PlayerStat movementSpeed = new PlayerStat();
    public PlayerStat drawBackDelay = new PlayerStat();
    public PlayerStat arrowSpeed = new PlayerStat();
    public PlayerStat flightTimeMax = new PlayerStat();
    public PlayerStat arrowDamage = new PlayerStat();

    // Array of all the stats
    private PlayerStat[] IncrementableStats;

    // public fields
    public bool repeater;
    public int coins;

    /// <summary>
    /// Default constructor
    /// Will give the starting stats
    /// </summary>
    public PlayerStats()
    {
        movementSpeed.name = "moveSpeed";
        drawBackDelay.name = "drawBackDelay";
        arrowSpeed.name = "arrowSpeed";
        flightTimeMax.name = "flightTimeMax";
        arrowDamage.name = "arrowDamage";

        IncrementableStats = new PlayerStat[] {
            movementSpeed,
            drawBackDelay,
            arrowSpeed,
            flightTimeMax,
            arrowDamage
        };

        movementSpeed.value = 600;
        drawBackDelay.value = 3;
        arrowSpeed.value = 8;
        flightTimeMax.value = 1;
        arrowDamage.value = 1;

        repeater = false;
        coins = 0;

        Shop.OnBuyStat += IncrementStat;
    }

    public void IncrementStat(object sender, ShopEventArgs e)
    {
        for (int i = 0; i < IncrementableStats.Length; i++)
        {
            if (IncrementableStats[i].name == e.statName)
            {
                IncrementableStats[i].IncrementStat();
                break;
            }
        }
    }
}
