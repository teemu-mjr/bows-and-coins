using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoucerAnimationController : MonoBehaviour
{
    // public fields
    public Animator boucerAnimator;
    public Animator chairAnimator;

    // private fields
    private Bouncer bouncer;
    private EnemyHealth enemyHealth;
   private void Awake()
    {
        bouncer = GetComponentInParent<Bouncer>();
        enemyHealth = GetComponentInParent<EnemyHealth>();

        bouncer.OnDraw += BoucerAnimationController_OnDraw;
        bouncer.OnShoot += BoucerAnimationController_OnShoot;
        enemyHealth.OnDamage += BoucerAnimationController_OnDamage;
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
        bouncer.OnDraw -= BoucerAnimationController_OnDraw;
        bouncer.OnShoot -= BoucerAnimationController_OnShoot;
        enemyHealth.OnDamage -= BoucerAnimationController_OnDamage;
    }
}
