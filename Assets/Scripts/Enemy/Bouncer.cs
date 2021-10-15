using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public float startupFreeze = 1f;
    public float minSpeed = 1f;
    public float maxSpeed = 5f;
    public float damage = 0.5f;
    public float knockBackForce = 10;
    public float speedX = 10;
    public float speedY = 10;
    public float shotInterval = 1;
    public GameObject arrow;
    public Transform firePoint;

    private GameObject target;
    private float timeAlive;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        speedX = Random.Range(-maxSpeed, maxSpeed);
        speedY = Random.Range(-maxSpeed, maxSpeed);
        InvokeRepeating("ShootArrow", Random.Range(0.5f, 2f), shotInterval);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeAlive > startupFreeze)
        {
            Move();
        }
        else
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
        if (collider.tag == "ColliderY")
        {
            speedY *= -1;
        }
        if (collider.tag == "ColliderX")
        {
            speedX *= -1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            collision.gameObject.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position).normalized * 250, ForceMode.Impulse);
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
