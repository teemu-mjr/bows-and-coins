using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChargeShader : MonoBehaviour
{
    public Material material;

    private void LateUpdate()
    {      
        material.SetFloat("_Amount", Bow.heldBackProcentage);
    }
}
