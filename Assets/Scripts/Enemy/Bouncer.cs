using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : Enemy
{
    public GameObject arrow;
    public float arrowSpeed;
    public Transform firePoint;

    private float maxSpeed = 5;
    private float minArrowSpeed = 5;
    private float maxArrowSpeed = 20;
    private float speedX = 10;
    private float speedY = 10;
    private float shotInterval = 1;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        speedX = Random.Range(-maxSpeed, maxSpeed);
        speedY = Random.Range(-maxSpeed, maxSpeed);
        arrowSpeed = Random.Range(minArrowSpeed, maxArrowSpeed);
        arrow.GetComponent<EnemyArrow>().speed = arrowSpeed;
        InvokeRepeating("ShootArrow", Random.Range(0.5f, 2f), shotInterval);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isFrozen)
        {
            Move();
        }
        else if (timeAlive > startupFreeze && isFrozen)
        {
            isFrozen = false;
        }
        else if (isFrozen)
        {
            timeAlive += Time.deltaTime;
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * speedY * Time.deltaTime);
        transform.Translate(Vector3.right * speedX * Time.deltaTime);
    }

    private void ShootArrow()
    {
        Vector3 shootDirection = target.transform.position - transform.position;
        firePoint.LookAt(target.transform.position);
        Instantiate(arrow, transform.position, firePoint.rotation);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "TriggerZ")
        {
            speedY *= -1;
        }
        if (collider.tag == "TriggerX")
        {
            speedX *= -1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !isFrozen)
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            collision.gameObject.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position).normalized * knockBackForce, ForceMode.Impulse);
            if (Random.Range(0, 2) == 0)
            {
                speedX *= -1;
            }
            else
            {
                speedY *= -1;
            }
        }
        if (collision.gameObject.tag == "Enemy")
        {
            if (Random.Range(0, 2) == 0)
            {
                speedX *= -1;
            }
            else
            {
                speedY *= -1;
            }
        }
    }
}
