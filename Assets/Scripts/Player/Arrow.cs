using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // private fields
    [HideInInspector] public float heldBackProcentage;
    private Rigidbody rb;
    private float fightTime;
    private float flightTimeMax;

    // audio
    public AudioClip arrowBreak;
    public AudioClip arrowHit;
    private PlaySound playSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Limit held back procentage to 1 (100%)
        if (heldBackProcentage > 1)
        {
            heldBackProcentage = 1;
        }

        rb.velocity = transform.forward * Player.stats.arrowSpeed.value * heldBackProcentage;

        flightTimeMax = Player.stats.flightTimeMax.value * heldBackProcentage;

        playSound = GetComponent<PlaySound>();
        playSound = playSound.Init();
    }

    private void FixedUpdate()
    {
        fightTime += Time.deltaTime;

        if (fightTime >= flightTimeMax)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.GetComponent<PlayerHealth>() || other.CompareTag("Particle"))
        {
            return;
        }

        if (other.gameObject.GetComponent<EnemyHealth>())
        {
            playSound.Play(arrowHit, true);
            Destroy(gameObject);
        }

        else if (other.transform.root.GetComponent<EnemyArrow>())
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            playSound.Play(arrowBreak, true);
        }
        else if (!other.transform.root.GetComponent<PlayerHealth>())
        {
            Destroy(gameObject);
            playSound.Play(arrowBreak, true);
        }

    }

    private void OnDestroy()
    {
        playSound.DestroyAudio();
    }
}
