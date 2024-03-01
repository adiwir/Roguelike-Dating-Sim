using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider slider;
    //[SerializeField] private Camera camera;
    //[SerializeField] private Transform target;

    public void UpdateHealthBar(int currentValue, int maxValue)
    {
        float currentVal = (float)currentValue;
        float maxVal = (float)maxValue;
        slider.value = currentVal/maxVal;
        //Debug.Log(slider.value);
        //Debug.Log(currentValue/maxValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
