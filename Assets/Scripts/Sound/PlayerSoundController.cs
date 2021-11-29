using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    // audio clips
    public AudioClip playerDamage;
    public AudioClip playerDeath;
    // private fields
    private AudioSource audioSource;

    private void Awake()
    {
        PlayerHealth.OnPlayerDamage += PlayerHealth_OnPlayerDamage;
        PlayerHealth.OnPlayerDeath += PlayerHealth_OnPlayerDeath;

        audioSource = GetComponent<AudioSource>();
    }

    private void PlayerHealth_OnPlayerDeath(object sender, System.EventArgs e)
    {
        PlaySound(playerDeath);
    }

    private void PlayerHealth_OnPlayerDamage(object sender, PlayerHealthArgs e)
    {
        PlaySound(playerDamage);
    }


    public void PlaySound(AudioClip clip, float volume = 1)
    {
        audioSource.volume = volume;
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDamage -= PlayerHealth_OnPlayerDamage;
        PlayerHealth.OnPlayerDeath -= PlayerHealth_OnPlayerDeath;
    }
}
