using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : Enemy
{
    // public fields
    public GameObject arrow;
    public GameObject rotatingShell;
    public Transform firePoint;

    // private fields
    private float maxSpeed = 5;
    private float arrowSpeed = 5;
    private float speedX = 10;
    private float speedY = 10;
    private bool isDrawn;

    // events
    public event System.EventHandler OnDraw;
    public event System.EventHandler OnShoot;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        speedX = Random.Range(-maxSpeed, maxSpeed);
        speedY = Random.Range(-maxSpeed, maxSpeed);
        arrowSpeed = ArenaController.enemyArrowSpeed.Value;
        arrow.GetComponent<EnemyArrow>().speed = arrowSpeed;
        isDrawn = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isFrozen)
        {
            Move();
            LookAtPlayer();
        }
        if (!isDrawn)
        {
            OnDraw?.Invoke(this, System.EventArgs.Empty);
            isDrawn = true;
        }
        else if (timeAlive > startupFreeze && isFrozen)
        {
            isFrozen = false;
            InvokeRepeating("ShootArrow", Random.Range(0.5f, 2f), ArenaController.enemyShotInterval.Value);
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

    private void LookAtPlayer()
    {
        rotatingShell.transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
    }

    private void ShootArrow()
    {
        Instantiate(arrow, firePoint.transform.position, firePoint.transform.rotation);
        OnShoot?.Invoke(this, System.EventArgs.Empty);
        isDrawn = false;
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
        if (collision.gameObject.CompareTag("TriggerZ"))
        {
            speedY *= -1;
        }
        else if (collision.gameObject.CompareTag("TriggerX"))
        {
            speedX *= -1;
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
