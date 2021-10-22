using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformOnNextWave : MonoBehaviour
{
    public Vector3 scaleToAdd;
    public Vector3 positionToAdd;

    private void Start()
    {
        ArenaController.OnNextWave += TransformObject;
    }

    private void TransformObject()
    {
        transform.position += positionToAdd;
        transform.localScale += scaleToAdd;
    }

    private void OnDestroy()
    {
        ArenaController.OnNextWave -= TransformObject;
    }
}
