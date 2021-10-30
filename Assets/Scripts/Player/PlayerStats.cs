using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    // player stats
    public PlayerStat movementSpeed;
    public PlayerStat drawBackDelay;
    public PlayerStat arrowSpeed;
    public PlayerStat flightTimeMax;
    public PlayerStat arrowDamage;
    // public fields
    public bool repeater;
    public int coins;

    /// <summary>
    /// Default constructor
    /// Will give the starting stats
    /// </summary>
    public PlayerStats()
    {
        movementSpeed = new PlayerStat() { name = "moveSpeed", value = 600, maxValue = 3000 };
        drawBackDelay = new PlayerStat() { name = "drawBackDelay", value = 3, maxValue = 0.1f };
        arrowSpeed = new PlayerStat() { name = "arrowSpeed", value = 8, maxValue = 50 };
        flightTimeMax = new PlayerStat() { name = "flightTimeMax", value = 0.5f, maxValue = 2 };
        arrowDamage = new PlayerStat() { name = "arrowDamage", value = 1, maxValue = 50 };
        repeater = false;
        coins = 0;

        Shop.OnBuyStat += IncrementStat;
    }

    public void IncrementStat(object sender, ShopEventArgs e)
    {
        switch (e.statName)
        {
            case "moveSpeed":
                movementSpeed.IncrementStat(50);
                break;
            case "drawBackDelay":
                drawBackDelay.DevideStat(2);
                break;
            case "arrowSpeed":
                arrowSpeed.IncrementStat(10);
                break;
            case "flightTimeMax":
                flightTimeMax.IncrementStat(1);
                break;
            case "arrowDamage":
                arrowDamage.IncrementStat(1);
                break;
            case "repeater":
                repeater = true;
                break;
            default:
                Debug.Log("Could not find the stat to increment");
                break;
        }
    }
}
