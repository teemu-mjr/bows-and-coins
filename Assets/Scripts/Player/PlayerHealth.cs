using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public override void Die()
    {
        Time.timeScale = 0;
    }

    public override void ShowDamage(float damage)
    {
        Debug.Log($"Player took: {damage}");
    }
}
