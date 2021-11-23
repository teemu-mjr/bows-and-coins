using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetPower(float power)
    {
        slider.value = power;

        fill.color = gradient.Evaluate(power);
    }
}
