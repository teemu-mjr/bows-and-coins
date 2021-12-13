using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    // public fields
    public float speed;
    public float flightTimeMax;

    // private fields
    private Rigidbody rb;
    private float fightTime;

    // audio
    public AudioClip bowShoot;
    public AudioClip arrowDestroy;
    private PlaySound playSound;

    // events
    public static event EventHandler OnShoot;

    void Start()
    {
        flightTimeMax = ArenaController.enemyArrowFightTime.Value;
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        OnShoot?.Invoke(this, EventArgs.Empty);


        playSound = GetComponent<PlaySound>();
        playSound = playSound.Init();

        playSound.Play(bowShoot, true);
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
        if (other.GetComponent<EnemyArrow>() || other.GetComponent<Enemy>())
        {
            return;
        }

        if (other.GetComponent<PlayerHealth>())
        {
            other.GetComponent<PlayerHealth>().TakeDamage(1);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
            playSound.Play(arrowDestroy, true);
        }
    }

    private void OnDisable()
    {
        playSound.DestroyAudio();
    }
}
