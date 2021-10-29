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

    // public fields
    public bool repeater;
    public int coins;

    /// <summary>
    /// Default constructor
    /// Will give the starting stats
    /// </summary>
    public PlayerStats()
    {
        movementSpeed.value = 600;
        drawBackDelay.value = 3;
        arrowSpeed.value = 8;
        flightTimeMax.value = 1;
        arrowDamage.value = 1;

        repeater = false;
        coins = 0;
    }


    //public float movementSpeed;
    //public float drawBackDelay;
    //public bool repeater;
    //public float arrowSpeed;
    //public float flightTimeMax;
    //public float arrowDamage;
    //public int coins;

    ///// <summary>
    ///// Default constructor
    ///// Will give the starting stats
    ///// </summary>
    //public PlayerStats()
    //{
    //    movementSpeed = 600;
    //    drawBackDelay = 3f;
    //    repeater = false;
    //    arrowSpeed = 8;
    //    flightTimeMax = 1f;
    //    arrowDamage = 1;
    //    coins = 0;
    //}
}
