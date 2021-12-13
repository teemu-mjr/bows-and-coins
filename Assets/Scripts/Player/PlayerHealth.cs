using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    // public fields
    public GameObject deadPlayer;

    // private fields
    private static bool isAlive = true;
    private int health = 3;

    // events
    public static event EventHandler<PlayerHealthArgs> OnPlayerDamage;
    public static event EventHandler OnPlayerDeath;

    // audio
    public AudioClip playerDamage;
    private PlaySound playSound;

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

        playSound = GetComponent<PlaySound>();
        playSound = playSound.Init();
    }

    public void TakeDamage(float damage)
    {
        health -= Mathf.RoundToInt(damage);
        playSound.Play(playerDamage, true);
        OnPlayerDamage?.Invoke(this, new PlayerHealthArgs() { health = this.health});
        if (health <= 0 && isAlive)
        {
            Die();
            playSound.DestroyAudio();
        }
    }

    public void Die()
    {
        isAlive = false;
        OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        gameObject.SetActive(false);
        Instantiate(deadPlayer, transform.position, transform.rotation);
    }
}
