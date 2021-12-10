using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSoundController : SoundController
{
    // audio clips
    public AudioClip coinPickup;
    public AudioClip Decline;

    private void Awake()
    {
        PlayerStats.OnCoinChange += PlayerStats_OnCoinChange;
        Shop.OnBuy += Shop_OnBuy;
        Shop.OnDecline += Shop_OnDecline;

        audioSource = GetComponent<AudioSource>();
    }

    private void Shop_OnBuy(object sender, System.EventArgs e)
    {
        PlaySound(coinPickup);
    }

    private void Shop_OnDecline(object sender, System.EventArgs e)
    {
        PlaySound(Decline);
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
        Shop.OnBuy -= Shop_OnBuy;
        Shop.OnDecline -= Shop_OnDecline;
    }
}
