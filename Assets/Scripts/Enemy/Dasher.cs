using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dasher : Enemy
{
    private float minDashDelay = 0.5f;
    private float maxDashDelay = 2;
    private float minDashInterval = 1;
    private float maxDashInterval = 3;

    private Rigidbody rb;
    private Vector3 playerDirection;
    private float dashDelay;
    private float dashInterval;
    private float speed = 75;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        speed = ArenaController.dasherSpeed.Value;

        RandomizeStats();

        InvokeRepeating("StartDash", Random.Range(0.5f, 2f), dashInterval);
    }


    private void RandomizeStats()
    {
        dashDelay = Random.Range(minDashDelay, maxDashDelay);
        dashInterval = Random.Range(minDashInterval, maxDashInterval);
    }

    private void StartDash()
    {
        playerDirection = new Vector3(target.transform.position.x, 0, target.transform.position.z) -
                new Vector3(transform.position.x, 0, transform.position.z);
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(dashDelay);
        rb.AddForce(playerDirection.normalized * speed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !isFrozen)
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(Damage);
            Vector3 targetVector = new Vector3(target.transform.position.x, 0, target.transform.position.z);
            collision.gameObject.GetComponent<Rigidbody>().AddForce((targetVector - new Vector3(transform.position.x, 0, transform.position.z)).normalized * knockBackForce, ForceMode.Impulse);
        }
    }
}
