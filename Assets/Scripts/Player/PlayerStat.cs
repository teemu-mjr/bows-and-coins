using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{
    // public fields
    public string name;
    public int level;
    public float value;
    public float maxValue;
    public int cost = 5;
    public bool maxed = false;

    // private fields
    private float addValue = 0;

    // propertyes
    public string UICost
    {
        get
        {
            if (!maxed)
            {
                return cost.ToString() + "$";

            }
            else
            {
                return "MAXED!";
            }
        }
    }


    public bool Increment()
    {
        if (addValue == 0)
        {
            addValue = (maxValue - value) / 20;
        }

        if (Player.stats.coins >= cost && !maxed)
        {
            Player.stats.coins -= cost;
            level++;
            cost += 5;

            if (value + addValue < maxValue)
            {
                value += addValue;
            }
            else
            {
                maxed = true;
                Debug.Log($"{name} MAXED!");
            }
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
            Player.stats.coins -= cost;
            level++;
            cost += 5;

            if (value / devideValue > maxValue)
            {
                value /= devideValue;
            }
            else
            {
                maxed = true;
                Debug.Log($"{name} MAXED!");
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
