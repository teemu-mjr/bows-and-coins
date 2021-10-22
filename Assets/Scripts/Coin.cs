using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(RandomVector3(-4, 4), ForceMode.Impulse);
        rb.rotation = Quaternion.Euler(RandomVector3(-360, 360));
    }

    private Vector3 RandomVector3(float maxValue, float minValue)
    {
        return new Vector3(Random.Range(maxValue, minValue), Random.Range(maxValue, minValue), Random.Range(maxValue, minValue));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(rb.isKinematic == false && other.CompareTag("Ground"))
        {
            rb.isKinematic = true;
        }
        if (other.CompareTag("Player"))
        {
            Player.stats.coins++;
            Destroy(gameObject);
        }
    }
}
