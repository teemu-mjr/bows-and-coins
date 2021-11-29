using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSoundController : MonoBehaviour
{
    // audio clips
    public AudioClip coinPickup;
    // private fields
    private AudioSource audioSource;

    private void Awake()
    {
        PlayerStats.OnCoinChange += PlayerStats_OnCoinChange;

        audioSource = GetComponent<AudioSource>();
    }

    private void PlayerStats_OnCoinChange(object sender, CoinEventArgs e)
    {
        if (e.value >= 1)
        {
            PlaySound(coinPickup);
        }
    }


    public void PlaySound(AudioClip clip, float volume = 1)
    {
        audioSource.volume = volume;
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void OnDisable()
    {
        PlayerStats.OnCoinChange -= PlayerStats_OnCoinChange;
    }
}
