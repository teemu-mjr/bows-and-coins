using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : SoundController
{
    // audio clips
    public AudioClip enemyShoot;
    public AudioClip enemyHit;
    public AudioClip enemyDeath;

    private void Awake()
    {
        EnemyArrow.OnShoot += EnemyArrow_OnShoot;
        EnemyHealth.OnDeath += EnemyHealth_OnDeath;

        audioSource = GetComponent<AudioSource>();
    }


    private void EnemyHealth_OnDeath(object sender, System.EventArgs e)
    {
        PlaySound(enemyDeath);
    }

    private void EnemyArrow_OnShoot(object sender, System.EventArgs e)
    {
        PlaySound(enemyShoot, 0.4f);
    }

    private void OnDisable()
    {
        EnemyArrow.OnShoot -= EnemyArrow_OnShoot;
        EnemyHealth.OnDeath -= EnemyHealth_OnDeath;
    }
}
