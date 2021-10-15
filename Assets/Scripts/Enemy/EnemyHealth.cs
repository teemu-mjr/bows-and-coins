using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public GameObject itemToDrop;
    public int dropAmount = 4;
    public override void Die()
    {
        DropItem();
        Destroy(gameObject);
    }

    private void DropItem()
    {
        for(int i = 0; i < dropAmount; i++)
        {
            Instantiate(itemToDrop, transform.position, transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerArrow"))
        {
            Arrow arrow = other.gameObject.GetComponent<Arrow>();
            TakeDamage(Player.stats.arrowDamage * arrow.heldBackProcentage);
        }
    }

}
