using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundObject : MonoBehaviour
{
    public void DestroySound()
    {
        StartCoroutine(DestroyWithDelay());
    }

    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
