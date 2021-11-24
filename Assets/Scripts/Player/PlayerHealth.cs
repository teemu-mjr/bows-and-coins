using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    // events
    public static event EventHandler<PlayerHealthArgs> OnPlayerDamage;
    public static event EventHandler OnPlayerDeath;

    // private fields
    private static bool isAlive = true;
    private int health = 3;

    // Propertyes
    public static bool IsAlive
    {
        get
        {
            return isAlive;
        }
    }

    private void Start()
    {
        isAlive = true;
    }

    public void TakeDamage(float damage)
    {
        health -= Mathf.RoundToInt(damage);
        OnPlayerDamage?.Invoke(this, new PlayerHealthArgs() { health = this.health});
        if (health <= 0 && isAlive)
        {
            Die();
        }
    }

    public void Die()
    {
        isAlive = false;
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
    }
}
