using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : Health
{
    // events
    public static event EventHandler OnPlayerDeath;

    // public fields
    public TextMeshProUGUI hpText;

    // private fields
    private static bool isAlive = true;

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
        UpdateHealthText(health);
        isAlive = true;
    }

    public override void TakeDamage(float damage)
    {
        health -= damage;
        UpdateHealthText(health);
        if (health <= 0 && isAlive)
        {
            Die();
        }
    }

    private void UpdateHealthText(float health)
    {
        hpText.text = $"HP: {health}";
    }

    public override void Die()
    {
        isAlive = false;
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
    }
}
