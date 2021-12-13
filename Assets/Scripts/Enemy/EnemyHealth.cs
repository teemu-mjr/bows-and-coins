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
    private bool isDead;

    public AudioClip damageSound;
    private PlaySound playSound;

    // events
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

        playSound = GetComponent<PlaySound>();
        playSound = playSound.Init();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        heathBar.GetComponent<HeathBar>().UpdateBar(health / ArenaController.enemyHealth.Value);
        if (health <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
        OnDamage?.Invoke(this, EventArgs.Empty);
    }

    public void Die()
    {
        DropItem();
        playSound.Play(damageSound, true);
        playSound.DestroyAudio();
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
