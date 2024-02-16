using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void UpdateHealthBar(float cur_Value, float max_Value)
    {
        slider.value = cur_Value/max_Value;
    }   
}
