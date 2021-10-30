using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{
    public string name;
    public int level;
    public float value;
    public float maxValue;

    public float cost;


    public void IncrementStat(float addValue)
    {
        Debug.Log($"{name} incremented");
        level++;
        cost += 100;

        if(value + addValue > 0)
        {
            value += addValue;
        }
    }

    public void DevideStat(float devideValue)
    {
        Debug.Log($"{name} devided");
        level++;
        cost += 100;

        if (value / devideValue > 0)
        {
            value /= devideValue;
        }
    }
}
