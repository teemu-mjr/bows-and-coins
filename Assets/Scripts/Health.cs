using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Health : MonoBehaviour
{
    public float health;

    public DmgPopUp dmgPopUp;

    /// <summary>
    /// Takes the given amount of damage
    /// If health reaches 0 the object will Die()
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        health -= damage;
        ShowDamage(damage);
        if (health <= 0)
        {
            Die();
        }
    }

    public abstract void ShowDamage(float damage);
    public abstract void Die();

}
