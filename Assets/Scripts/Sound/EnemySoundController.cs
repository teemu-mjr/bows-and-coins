using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    // audio clips
    public AudioClip enemyShoot;
    public AudioClip enemyHit;
    public AudioClip enemyDeath;
    public AudioClip enemySpawn;
    // private fields
    private AudioSource audioSource;

    private void Awake()
    {
        EnemyArrow.OnShoot += EnemyArrow_OnShoot;
        EnemyHealth.OnDeath += EnemyHealth_OnDeath;
        ArenaController.OnNextWave += ArenaController_OnNextWave;

        audioSource = GetComponent<AudioSource>();
    }

    private void ArenaController_OnNextWave(object sender, WaveEventArgs e)
    {
        PlaySound(enemySpawn);
    }

    private void EnemyHealth_OnDeath(object sender, System.EventArgs e)
    {
        PlaySound(enemyDeath);
    }

    private void EnemyArrow_OnShoot(object sender, System.EventArgs e)
    {
        PlaySound(enemyShoot);
    }

    public void PlaySound(AudioClip clip, float volume = 1)
    {
        audioSource.volume = volume;
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void OnDisable()
    {
        EnemyArrow.OnShoot -= EnemyArrow_OnShoot;
        EnemyHealth.OnDeath -= EnemyHealth_OnDeath;
        ArenaController.OnNextWave -= ArenaController_OnNextWave;
    }
}
