using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSoundController : SoundController
{
    // audio clips
    public AudioClip coinPickup;

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

    private void OnDisable()
    {
        PlayerStats.OnCoinChange -= PlayerStats_OnCoinChange;
    }
}
