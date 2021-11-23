using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    public float speed;
    public float flightTimeMax = 1;

    private Rigidbody rb;
    private float fightTime;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
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
        if (!other.GetComponent<EnemyArrow>() && !GetComponent<Enemy>())
        {
            if (other.GetComponent<PlayerHealth>())
            {
                other.GetComponent<PlayerHealth>().TakeDamage(1);
            }
            Destroy(gameObject);
        }
    }
}
