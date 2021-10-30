using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject itemToDrop;

    private float health = ArenaController.enemyHealth.Value;
    private int dropAmount = 3;

    private void Start()
    {
        dropAmount = Mathf.RoundToInt(ArenaController.coinDropAmount.Value);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
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
            TakeDamage(Player.stats.arrowDamage.value * arrow.heldBackProcentage);
        }
    }

}
