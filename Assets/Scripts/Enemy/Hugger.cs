using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hugger : Enemy
{
    // private fields
    private float minSpeed = 1f;
    private float maxSpeed;
    private float speed;
    private Vector3 playerPos;
    private Rigidbody rb;
    private float attackCooldown;

    // events
    public event System.EventHandler OnDamagePlayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        maxSpeed = ArenaController.huggerSpeed.Value;
        target = GameObject.FindGameObjectWithTag("Player");
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void FixedUpdate()
    {
        if (!isFrozen)
        {
            FollowPlayer();
            if (attackCooldown < 2)
            {
                attackCooldown += Time.deltaTime;
            }
            else
            {

            }
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

    void FollowPlayer()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 0.1f)
        {
            playerPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.LookAt(playerPos);
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(transform.forward * (speed * 60));
            }
        }
        if (Vector3.Distance(transform.position, target.transform.position) < 2 && attackCooldown >= 2)
        {
            OnDamagePlayer?.Invoke(this, System.EventArgs.Empty);
            attackCooldown = 0;
        }
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
