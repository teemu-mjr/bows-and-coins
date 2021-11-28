using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // public fields
    public AudioClip hitEnemy;
    public AudioClip hitWall;

    // private fields
    [HideInInspector] public float heldBackProcentage;

    private Rigidbody rb;
    private float fightTime;
    private float flightTimeMax;

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
        if (other.transform.root.GetComponent<Enemy>())
        {
            Destroy(gameObject);
        }
        else if (other.transform.root.GetComponent<EnemyArrow>())
        {
            Destroy(other.gameObject);
        }
        else if (!other.transform.root.GetComponent<PlayerHealth>())
        {
            Destroy(gameObject);
        }

    }
}
