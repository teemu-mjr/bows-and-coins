using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    public float movementSpeed;
    public float drawBackDelay;
    public bool repeater;
    public float arrowSpeed;
    public float flightTimeMax;
    public float arrowDamage;
    public int coins;

    /// <summary>
    /// Default constructor
    /// Will give the starting stats
    /// </summary>
    public PlayerStats()
    {
        movementSpeed = 100;
        drawBackDelay = 3f;
        repeater = false;
        arrowSpeed = 10;
        flightTimeMax = 1f;
        arrowDamage = 1;
        coins = 0;
    }

    public PlayerStats(float movementSpeed, float drawBackDelay, bool repeater, float arrowSpeed, float flightTimeMax, float arrowDamage)
    {
        this.movementSpeed = movementSpeed;
        this.drawBackDelay = drawBackDelay;
        this.repeater = repeater;
        this.arrowSpeed = arrowSpeed;
        this.flightTimeMax = flightTimeMax;
        this.arrowDamage = arrowDamage;
    }

}
