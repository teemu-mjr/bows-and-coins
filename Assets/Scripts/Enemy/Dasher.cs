using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dasher : MonoBehaviour
{
    public float startupFreeze = 1f;
    public float minSpeed = 1f;
    public float maxSpeed = 5f;
    public float damage = 0.5f;
    public float knockBackForce = 10;
    public float speed = 10;
    public float minDashDelay = 1;
    public float maxDashDelay = 3;
    public float minDashInterval = 1;
    public float maxDashInterval = 3;

    private GameObject target;
    private Rigidbody rb;
    private Vector3 playerDirection;
    private float dashDelay;
    private float dashInterval;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position).normalized * knockBackForce, ForceMode.Impulse);
        }
    }
}
