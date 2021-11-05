using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{
    public string name;
    public int level;
    public float value;
    public float maxValue;
    public int cost = 5;

    public bool maxed = false;

    public bool Increment(float addValue)
    {
        if (Player.stats.coins >= cost && !maxed)
        {
            level++;
            cost += 5;

            if (value + addValue <= maxValue)
            {
                value += addValue;
            }
            else
            {
                maxed = true;
                Debug.Log($"{name} MAXED!");
            }
            Player.stats.coins -= cost;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Devide(float devideValue)
    {
        if (Player.stats.coins >= cost && !maxed)
        {
            level++;
            cost += 5;

            if (value / devideValue >= maxValue)
            {
                value /= devideValue;
            }
            else
            {
                maxed = true;
                Debug.Log($"{name} MAXED!");
            }
            Player.stats.coins -= cost;
            return true;
        }
        else
        {
            return false;
        }
    }
}
