using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat
{
    public string name;
    public int level;
    public float value;
    public float cost;

    public void IncrementStat()
    {
        Debug.Log($"{name} incremented");
    }
}
