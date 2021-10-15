using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithTag : MonoBehaviour
{
    public List<string> tags;

    private void OnTriggerEnter(Collider other)
    {
        if (tags.Contains(other.tag))
        {
            Destroy(gameObject);
        }
    }
}
