using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // public fields
    public GameObject target;
    public float startupFreeze = 1f;
    public bool isFrozen = true;
    public float knockBackForce = 100;
    public float timeAlive = 0;

    // private fields
    private float damage = 1;

    public float Damage
    {
        get
        {
            return damage;
        }
    }

    private void FixedUpdate()
    {
        if (timeAlive > startupFreeze && isFrozen)
        {
            isFrozen = false;
        }
        else if (isFrozen)
        {
            timeAlive += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !isFrozen)
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(Damage);
            collision.gameObject.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position).normalized * knockBackForce, ForceMode.Impulse);
            gameObject.GetComponent<Rigidbody>().AddForce((transform.position - target.transform.position).normalized * knockBackForce, ForceMode.Impulse);
        }
    }
}
