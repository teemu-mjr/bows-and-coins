using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformCameraOnNextWave : MonoBehaviour
{
    public float growAmount;
    private void Start()
    {
        ArenaController.OnNextWave += TransformObject;
    }

    private void TransformObject()
    {
        Camera.main.orthographicSize += growAmount;
    }

    private void OnDestroy()
    {
        ArenaController.OnNextWave -= TransformObject;
    }
}
