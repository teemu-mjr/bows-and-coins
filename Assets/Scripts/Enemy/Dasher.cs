using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dasher : Enemy
{
    private float minDashDelay = 0.5f;
    private float maxDashDelay = 2;
    private float minDashInterval = 1;
    private float maxDashInterval = 3;
    private float minSpeed = 5;
    private float maxSpeed = 30;

    private Rigidbody rb;
    private Vector3 playerDirection;
    private float dashDelay;
    private float dashInterval;
    private float speed;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();

        RandomizeStats();

        InvokeRepeating("StartDash", Random.Range(0.5f, 2f), dashInterval);
    }


    private void RandomizeStats()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        dashDelay = Random.Range(minDashDelay, maxDashDelay);
        dashInterval = Random.Range(minDashInterval, maxDashInterval);
    }

    private void StartDash()
    {
        playerDirection = (target.transform.position - transform.position).normalized;
        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(dashDelay);
        rb.AddForce(playerDirection * speed, ForceMode.Impulse);
    }
}
