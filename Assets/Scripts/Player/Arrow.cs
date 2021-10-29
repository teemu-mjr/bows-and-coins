using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [HideInInspector] public float heldBackTime;
    [HideInInspector] public float heldBackProcentage;

    private Rigidbody rb;
    private float fightTime;
    private float flightTimeMax;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        heldBackProcentage = heldBackTime / Player.stats.drawBackDelay.value;

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
        if (!other.CompareTag("Player") && !other.CompareTag("PlayerArrow"))
        {
            Destroy(gameObject);
            if (other.CompareTag("EnemyArrow"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
