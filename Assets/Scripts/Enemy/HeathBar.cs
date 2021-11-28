using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathBar : MonoBehaviour
{
    public Transform fillTranform;

    public void UpdateBar(float amount)
    {
        fillTranform.localScale = new Vector3(amount, fillTranform.transform.localScale.y, fillTranform.transform.localScale.z);
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
