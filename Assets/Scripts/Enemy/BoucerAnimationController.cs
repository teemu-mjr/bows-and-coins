using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoucerAnimationController : MonoBehaviour
{
    public Animator boucerAnimator;
    public Animator chairAnimator;
   private void Awake()
    {
        GetComponentInParent<Bouncer>().OnDraw += BoucerAnimationController_OnDraw;
        GetComponentInParent<Bouncer>().OnShoot += BoucerAnimationController_OnShoot;
        GetComponentInParent<EnemyHealth>().OnDamage += BoucerAnimationController_OnDamage;
    }


    private void BoucerAnimationController_OnShoot(object sender, System.EventArgs e)
    {
        boucerAnimator.SetTrigger("Shoot");
    }

    private void BoucerAnimationController_OnDraw(object sender, System.EventArgs e)
    {
        boucerAnimator.SetTrigger("Draw");
    }
    private void BoucerAnimationController_OnDamage(object sender, System.EventArgs e)
    {
        chairAnimator.SetTrigger("Damage");
    }

    private void OnDisable()
    {
        GetComponentInParent<Bouncer>().OnDraw -= BoucerAnimationController_OnDraw;
        GetComponentInParent<Bouncer>().OnShoot -= BoucerAnimationController_OnShoot;
    }
}
