using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // public fields
    public GameObject itemToDrop;

    public AudioSource audioSource;
    public AudioClip hitAudio;

    // private fields
    private float health;
    private int dropAmount = 3;

    private void Start()
    {
        health = ArenaController.enemyHealth.Value;
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
            audioSource.PlayOneShot(hitAudio);
            Arrow arrow = other.gameObject.GetComponent<Arrow>();
            TakeDamage(Player.stats.arrowDamage.value * arrow.heldBackProcentage);
        }
    }

}
