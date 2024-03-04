using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void UpdateHealthBar(int currentValue, int maxValue)
    {
        float currentVal = (float)currentValue;
        float maxVal = (float)maxValue;
        slider.value = currentVal/maxVal;
        Debug.Log(slider.value);
        Debug.Log(currentValue/maxValue);
    }
}
