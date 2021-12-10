using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSoundController : SoundController
{
    // audio clips
    public AudioClip wallHit;
    public AudioClip draw;
    public AudioClip bowReady;

    private void Awake()
    {
        Bow.OnDraw += Bow_OnDraw;
        Bow.OnReady += Bow_OnReady;
        Arrow.OnHitWall += Arrow_OnHitWall;

        audioSource = GetComponent<AudioSource>();
    }

    private void Bow_OnReady(object sender, System.EventArgs e)
    {
        PlaySound(bowReady);
    }

    private void Bow_OnDraw(object sender, System.EventArgs e)
    {
        PlaySound(draw);
    }

    private void Arrow_OnHitWall(object sender, System.EventArgs e)
    {
        PlaySound(wallHit);
    }

    private void OnDisable()
    {
        Bow.OnDraw -= Bow_OnDraw;
        Bow.OnReady -= Bow_OnReady;
        Arrow.OnHitWall -= Arrow_OnHitWall;
    }
}
