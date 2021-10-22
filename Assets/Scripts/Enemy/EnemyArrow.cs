using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("EnemyArrow") && !other.CompareTag("Enemy"))
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Health>().TakeDamage(1);
            }
            Destroy(gameObject);
        }
    }
}
