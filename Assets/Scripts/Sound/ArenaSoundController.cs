using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaSoundController : SoundController
{
    // audio clips
    public AudioClip enemySpawn;

    private void Awake()
    {
        ArenaController.OnNextWave += ArenaController_OnNextWave;

        audioSource = GetComponent<AudioSource>();
    }
    private void ArenaController_OnNextWave(object sender, WaveEventArgs e)
    {
        PlaySound(enemySpawn, 0.2f);
    }

    private void OnDisable()
    {
        ArenaController.OnNextWave -= ArenaController_OnNextWave;
    }

}
