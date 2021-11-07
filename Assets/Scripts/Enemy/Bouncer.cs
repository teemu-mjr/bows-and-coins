using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : Enemy
{
    public GameObject arrow;
    public Transform firePoint;

    private float maxSpeed = 5;
    private float arrowSpeed = 5;
    private float speedX = 10;
    private float speedY = 10;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        speedX = Random.Range(-maxSpeed, maxSpeed);
        speedY = Random.Range(-maxSpeed, maxSpeed);
        arrowSpeed = ArenaController.enemyArrowSpeed.Value;
        arrow.GetComponent<EnemyArrow>().speed = arrowSpeed;
        InvokeRepeating("ShootArrow", Random.Range(0.5f, 2f), ArenaController.enemyShotInterval.Value);
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
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(Damage);
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
