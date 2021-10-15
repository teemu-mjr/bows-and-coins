using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private float fightTime;
    private float flightTimeMax;
    public float heldBackProcentage;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        heldBackProcentage = Bow.heldBackTime / Player.stats.drawBackDelay;
        if (heldBackProcentage > 1)
        {
            heldBackProcentage = 1;
        }
        rb.velocity = transform.forward * Player.stats.arrowSpeed * heldBackProcentage;
        Bow.heldBackTime = 0;

        flightTimeMax = Player.stats.flightTimeMax * heldBackProcentage;
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
        }
    }
}
