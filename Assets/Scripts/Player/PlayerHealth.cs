using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    // events
    public static event EventHandler<HealthArgs> OnPlayerDamage;
    public static event EventHandler OnPlayerDeath;

    // HealthArgs
    public class HealthArgs : EventArgs
    {
        public float health;

        public HealthArgs(float health)
        {
            this.health = health;
        }
    }

    // private fields
    private static bool isAlive = true;
    private float health = 3;

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
        health -= damage;
        OnPlayerDamage?.Invoke(this, new HealthArgs(health));
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
