using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // public fields
    public GameObject coin;
    public GameObject body;
    public GameObject heathBar;
    public GameObject bodyTransformObject;

    // private fields
    private float health;
    private int dropAmount;
    private Transform coinTransform;

    // events
    public static event EventHandler OnDeath;
    public event EventHandler OnDamage;

    private void Start()
    {
        if (!bodyTransformObject)
        {
            bodyTransformObject = transform.gameObject;
        }
        health = ArenaController.enemyHealth.Value;
        dropAmount = Mathf.RoundToInt(ArenaController.coinDropAmount.Value);
        coin.GetComponent<Coin>().coinValue = Mathf.RoundToInt(ArenaController.coinValue.Value);
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
        else
        {
            OnDamage?.Invoke(this, EventArgs.Empty);
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
            Instantiate(coin, transform.position, transform.rotation, coinTransform);
        }
        if (body)
        {
            Instantiate(body, bodyTransformObject.transform);
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
