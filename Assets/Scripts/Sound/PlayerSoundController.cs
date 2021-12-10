using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : SoundController
{
    // audio clips
    public AudioClip playerDamage;

    private void Awake()
    {
        PlayerHealth.OnPlayerDamage += PlayerHealth_OnPlayerDamage;
        PlayerHealth.OnPlayerDeath += PlayerHealth_OnPlayerDeath;

        audioSource = GetComponent<AudioSource>();
    }

    private void PlayerHealth_OnPlayerDeath(object sender, System.EventArgs e)
    {
        PlaySound(playerDamage);
    }

    private void PlayerHealth_OnPlayerDamage(object sender, PlayerHealthArgs e)
    {
        PlaySound(playerDamage);
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDamage -= PlayerHealth_OnPlayerDamage;
        PlayerHealth.OnPlayerDeath -= PlayerHealth_OnPlayerDeath;
    }
}
