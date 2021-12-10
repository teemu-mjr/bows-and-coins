using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowSoundController : SoundController
{
    // audio clips
    public AudioClip playerShoot;

    private void Awake()
    {
        Bow.OnShoot += Bow_OnShoot;       

        audioSource = GetComponent<AudioSource>();
    }

    private void Bow_OnShoot(object sender, System.EventArgs e)
    {
        PlaySound(playerShoot);
    }


    private void OnDisable()
    {
        Bow.OnShoot -= Bow_OnShoot;
    }
}
