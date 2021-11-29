using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowSoundController : MonoBehaviour
{
    // audio clips
    public AudioClip playerShoot;
    public AudioClip enemyShoot;
    public AudioClip wallHit;
    // private fields
    private AudioSource audioSource;

    private void Awake()
    {
        Bow.OnShoot += Bow_OnShoot;
        EnemyArrow.OnShoot += EnemyArrow_OnShoot;
        Arrow.OnHitWall += Arrow_OnHitWall;

        audioSource = GetComponent<AudioSource>();
    }

    private void Arrow_OnHitWall(object sender, System.EventArgs e)
    {
        PlaySound(wallHit);
    }

    private void EnemyArrow_OnShoot(object sender, System.EventArgs e)
    {
        PlaySound(enemyShoot, 0.2f);
    }

    private void Bow_OnShoot(object sender, System.EventArgs e)
    {
        PlaySound(playerShoot);
    }


    public void PlaySound(AudioClip clip, float volume = 1)
    {
        audioSource.volume = volume;
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void OnDisable()
    {
        Bow.OnShoot -= Bow_OnShoot;
        EnemyArrow.OnShoot -= EnemyArrow_OnShoot;
        Arrow.OnHitWall -= Arrow_OnHitWall;
    }
}
