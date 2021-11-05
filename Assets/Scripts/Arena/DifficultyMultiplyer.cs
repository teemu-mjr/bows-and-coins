using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyMultiplyer
{
    // props
    public float Value { get { return value; } }

    // private fields
    private float value;
    private float incrementingValue;
    private float maximumValue;
    private bool isDecrementing;

    public DifficultyMultiplyer(float statingValue, float incrementingValue, float maxValue, bool isDecrementing = false)
    {
        this.value = statingValue;
        this.incrementingValue = incrementingValue;
        this.maximumValue = maxValue;
        this.isDecrementing = isDecrementing;
    }

    public void Increment()
    {
        if (value + incrementingValue < maximumValue && !isDecrementing)
        {
            value += incrementingValue;
        }
        else if (value - incrementingValue > maximumValue && isDecrementing)
        {
            value -= incrementingValue;
        }
    }
}
