using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//deals with health bar animation
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    //sets the maximum health any given bar can represent
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }
    //updates health on the bar whenever it is changed
    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
