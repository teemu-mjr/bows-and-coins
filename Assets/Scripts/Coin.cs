using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // public fields
    public int coinValue;

    // private fields
    private Rigidbody rb;

    // events
    //public static event System.EventHandler<CoinEventArgs> OnCoinPickup;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(RandomVector3(-4, 4), ForceMode.Impulse);
        rb.AddTorque(RandomVector3(-1000, 10000), ForceMode.Impulse);
    }

    private Vector3 RandomVector3(float maxValue, float minValue)
    {
        return new Vector3(Random.Range(maxValue, minValue), Random.Range(maxValue, minValue), Random.Range(maxValue, minValue));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (rb.isKinematic == false && other.CompareTag("Ground"))
        {
            rb.isKinematic = true;
        }
        if (other.CompareTag("Wall"))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        if (other.CompareTag("Player"))
        {;
            Player.stats.AddCoins(coinValue);
            //OnCoinPickup?.Invoke(this, new CoinEventArgs() { value = coinValue});
            Destroy(gameObject);
        }
    }
}
