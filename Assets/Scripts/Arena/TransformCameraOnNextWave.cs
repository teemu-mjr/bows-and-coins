using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformCameraOnNextWave : MonoBehaviour
{
    public float growAmount;
    public Vector3 moveAmount;
    private void Start()
    {
        ArenaController.OnNextWave += TransformObject;
    }

    private void TransformObject(object sender, EventArgs e)
    {
        if(Camera.main.orthographic == true)
        {
            Camera.main.orthographicSize += growAmount;
        }
        else
        {
            Camera.main.transform.position += moveAmount;
        }
    }

    private void OnDestroy()
    {
        ArenaController.OnNextWave -= TransformObject;
    }
}
