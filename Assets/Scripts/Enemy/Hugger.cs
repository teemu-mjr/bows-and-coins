using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hugger : MonoBehaviour
{
    public float startupFreeze = 1f;
    public float minSpeed = 1f;
    public float maxSpeed = 5f;
    public float damage = 0.5f;
    public float knockBackForce = 10;
    public float speed;

    private GameObject target;
    private float timeAlive;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void FixedUpdate()
    {
        if (timeAlive > startupFreeze)
        {
            FollowPlayer();
        }
        else
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position).normalized * knockBackForce, ForceMode.Impulse);
        }
    }
}
