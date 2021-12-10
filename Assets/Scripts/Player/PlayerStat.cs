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
    public int cost = 2;
    public bool maxed = false;
    public bool inUse = true;
    public float addValue = 0;

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

    public string UIToggle
    {
        get
        {
            if (inUse)
            {
                return "ON";
            }
            else
            {
                return "OFF";
            }
        }
    }


    public bool Increment(float addValue = 0)
    {
        if (addValue == 0 && this.addValue == 0)
        {
            this.addValue = (maxValue - value) / 20;
        }
        else if (addValue != 0)
        {
            this.addValue = addValue;
        }

        if (Player.stats.Coins >= cost && !maxed)
        {
            Player.stats.RemoveCoins(cost);
            level++;
            cost += 4;

            if (value + this.addValue < maxValue)
            {
                value += this.addValue;
            }
            else
            {
                maxed = true;
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
        if (Player.stats.Coins >= cost && !maxed)
        {
            Player.stats.RemoveCoins(cost);
            level++;
            cost += 4;

            if (value / devideValue > maxValue)
            {
                value /= devideValue;
            }
            else
            {
                maxed = true;
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
