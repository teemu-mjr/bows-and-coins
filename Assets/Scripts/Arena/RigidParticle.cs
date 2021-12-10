using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidParticle : MonoBehaviour
{
    public float removeDelay;

    private static Transform particles;
    private Rigidbody rb;

    void Start()
    {
        particles = GameObject.Find("Particles").transform;

        transform.parent = particles;

        transform.gameObject.layer = LayerMask.NameToLayer("Particle");

        if (!this.gameObject.GetComponent<Rigidbody>())
        {
            this.gameObject.AddComponent<Rigidbody>();
        }

        rb = GetComponent<Rigidbody>();
        rb.AddForce(RandomVector3(-2, 2), ForceMode.Impulse);
        rb.AddTorque(RandomVector3(-1000, 10000), ForceMode.Impulse);


        StartCoroutine(RemoveWithDelay());
    }

    private Vector3 RandomVector3(float maxValue, float minValue)
    {
        return new Vector3(Random.Range(maxValue, minValue), Random.Range(maxValue, minValue), Random.Range(maxValue, minValue));
    }

    private IEnumerator RemoveWithDelay()
    {
        yield return new WaitForSeconds(removeDelay);
        Destroy(this.gameObject);
    }
}
