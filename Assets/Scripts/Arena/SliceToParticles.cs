using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceToParticles : MonoBehaviour
{
    private void Start()
    {
    }

    public void Slice()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Instantiate(transform.GetChild(i).gameObject, transform);
        }

        Destroy(this.gameObject);
    }
}
