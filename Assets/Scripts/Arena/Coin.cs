using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // public fields
    public int coinValue;

    // private fields
    private Rigidbody rb;

    // audio
    public AudioClip coinPickUp;
    private PlaySound playSound;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(RandomVector3(-2, 2), ForceMode.Impulse);
        rb.AddTorque(RandomVector3(-1000, 10000), ForceMode.Impulse);

        playSound = GetComponent<PlaySound>();
        playSound = playSound.Init();
    }

    private Vector3 RandomVector3(float maxValue, float minValue)
    {
        return new Vector3(Random.Range(maxValue, minValue), Random.Range(maxValue, minValue), Random.Range(maxValue, minValue));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.stats.AddCoins(coinValue);
            playSound.Play(coinPickUp, true);
            playSound.DestroyAudio();            
            Destroy(gameObject);
        }
        if (rb.isKinematic == false && other.CompareTag("Ground"))
        {
            rb.isKinematic = true;
        }
        else if (other.CompareTag("TriggerZ") || other.CompareTag("TriggerX"))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    private void OnDestroy()
    {
        playSound.DestroyAudio();
    }
}
