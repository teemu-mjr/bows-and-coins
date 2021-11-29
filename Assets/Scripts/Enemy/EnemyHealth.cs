using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // public fields
    public GameObject itemToDrop;
    public GameObject heathBar;

    // private fields
    private float health;
    private int dropAmount;
    private Transform coinTransform;

    // events
    public static event EventHandler OnDeath;

    private void Start()
    {
        health = ArenaController.enemyHealth.Value;
        dropAmount = Mathf.RoundToInt(ArenaController.coinDropAmount.Value);
        itemToDrop.GetComponent<Coin>().coinValue = Mathf.RoundToInt(ArenaController.coinValue.Value);
        coinTransform = GameObject.Find("Coins").transform;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        heathBar.GetComponent<HeathBar>().UpdateBar(health / ArenaController.enemyHealth.Value);
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        DropItem();
        OnDeath?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }

    private void DropItem()
    {
        for (int i = 0; i < dropAmount; i++)
        {
            Instantiate(itemToDrop, transform.position, transform.rotation, coinTransform);
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
