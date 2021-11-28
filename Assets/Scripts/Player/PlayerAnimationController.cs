using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    // pubic fields
    public PlayerMovement playerMovement;

    // private fields
    private Animator animator;
    private bool isDrawn = false;
    private Rigidbody rb;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        Bow.OnShoot += Bow_OnShoot;
    }

    private void Start()
    {
        animator.SetFloat("DrawSpeed", 1 / Player.stats.drawBackDelay.value);
        
    }

    private void FixedUpdate()
    {
        animator.SetFloat("MoveVector", playerMovement.movementVector.magnitude);

        animator.SetFloat("MoveSpeed", rb.velocity.magnitude / 3.75f);

        animator.SetBool("Shooting", Bow.isShooting);

        if (Bow.isShooting && !isDrawn)
        {
            animator.SetTrigger("DrawBow");
            isDrawn = true;
        }
        else if (!Bow.isShooting && isDrawn)
        {
            isDrawn = false;
        }
    }
    private void Bow_OnShoot(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Shoot");
        isDrawn = false;
    }
    private void OnDisable()
    {
        Bow.OnShoot -= Bow_OnShoot;
    }

}
