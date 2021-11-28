using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuggerAnimationController : MonoBehaviour
{
    // private fields
    private Animator animator;
    private Rigidbody rb;
    private Hugger hugger;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        hugger = GetComponent<Hugger>();

        hugger.OnDamagePlayer += Enemy_OnDamagePlayer;
    }

    private void Enemy_OnDamagePlayer(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Attack");
    }

    private void FixedUpdate()
    {
        animator.SetFloat("MoveSpeed", rb.velocity.magnitude);
    }

    private void OnDisable()
    {
        hugger.OnDamagePlayer -= Enemy_OnDamagePlayer;
    }
}
