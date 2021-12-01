using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoucerAnimationController : MonoBehaviour
{
    private Animator animator;
   private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        GetComponentInParent<Bouncer>().OnDraw += BoucerAnimationController_OnDraw;
        GetComponentInParent<Bouncer>().OnShoot += BoucerAnimationController_OnShoot;
    }

    private void BoucerAnimationController_OnShoot(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Shoot");
    }

    private void BoucerAnimationController_OnDraw(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Draw");
    }

    private void OnDisable()
    {
        GetComponentInParent<Bouncer>().OnDraw -= BoucerAnimationController_OnDraw;
        GetComponentInParent<Bouncer>().OnShoot -= BoucerAnimationController_OnShoot;
    }
}
