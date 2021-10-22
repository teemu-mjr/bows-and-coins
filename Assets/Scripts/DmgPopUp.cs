using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DmgPopUp : MonoBehaviour
{
    private TextMeshProUGUI dmgPopUp;

    public void Init()
    {
    }

    public void PopUp(float damage)
    {
        dmgPopUp.text = "10";
        Instantiate(dmgPopUp, new Vector3(transform.position.x, 3, transform.position.z), Quaternion.Euler(new Vector3(90, 0, 0)));
    }
}
