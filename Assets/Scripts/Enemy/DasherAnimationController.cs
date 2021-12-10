using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DasherAnimationController : MonoBehaviour
{
    // public fields
    public Animator animator;

    // private fields
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > 0.2f)
        {
            animator.SetBool("Attacking", true);
        }
        else if (animator.GetBool("Attacking") != false)
        {
            animator.SetBool("Attacking", false);
        }
    }
}
