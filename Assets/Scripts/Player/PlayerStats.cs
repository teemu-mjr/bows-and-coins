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
    public PlayerStat repeater;

    // public fields
    public int coins;

    /// <summary>
    /// Default constructor
    /// Will give the starting stats
    /// </summary>
    public PlayerStats()
    {
        movementSpeed = new PlayerStat() { name = "moveSpeed", value = 500, maxValue = 1000 };
        drawBackDelay = new PlayerStat() { name = "drawBackDelay", value = 3, maxValue = 0.1f };
        arrowSpeed = new PlayerStat() { name = "arrowSpeed", value = 5, maxValue = 55 };
        flightTimeMax = new PlayerStat() { name = "flightTimeMax", value = 0.5f, maxValue = 2 };
        arrowDamage = new PlayerStat() { name = "arrowDamage", value = 1, maxValue = 50 };
        repeater = new PlayerStat() { name = "repeater", value = 0, maxValue = 1, cost = 250};
        coins = 0;
    }

    public bool IncrementStat(string statName)
    {
        bool couldBoy = false;
        switch (statName)
        {
            case "moveSpeed":
                couldBoy = movementSpeed.Increment();
                break;
            case "drawBackDelay":
                couldBoy = drawBackDelay.Devide(1.188f);
                break;
            case "arrowSpeed":
                couldBoy = arrowSpeed.Increment();
                break;
            case "flightTimeMax":
                couldBoy = flightTimeMax.Increment();
                break;
            case "arrowDamage":
                couldBoy = arrowDamage.Increment();
                break;
            case "repeater":
                couldBoy = repeater.Increment(10);
                break;
            default:
                Debug.Log("Could not find the stat to increment");
                return false;
        }
        if (couldBoy)
        {
            IncrementCost(1);
        }

        return couldBoy;
    }

    private void IncrementCost(int amount)
    {
        movementSpeed.cost += amount;
        drawBackDelay.cost += amount;
        arrowSpeed.cost += amount;
        flightTimeMax.cost += amount;
        arrowDamage.cost += amount;
    }


}   
