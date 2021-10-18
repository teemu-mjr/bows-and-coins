using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hugger : Enemy
{
    private float minSpeed = 1f;
    private float maxSpeed = 5f;
    private float speed;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void FixedUpdate()
    {
        if (!isFrozen)
        {
            FollowPlayer();
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
            Vector3 movementDirection = target.transform.position - transform.position;
            transform.Translate(movementDirection.normalized * speed * Time.deltaTime);
        }
    }
}
