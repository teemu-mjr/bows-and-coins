using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : Health
{
    public static event EventHandler OnPlayerDeath;

    public TextMeshProUGUI hpText;


    private void Start()
    {
        UpdateHealthText(health);
    }
    public override void TakeDamage(float damage)
    {
        health -= damage;
        UpdateHealthText(health);
        if (health <= 0)
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
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
    }
}
