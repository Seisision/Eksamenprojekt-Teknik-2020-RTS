using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fillImage;

    public void UpdateColor(float percentage)
    {
        Color c = new Color(0,0, 0);
        c.r = 1 - percentage;
        c.g = percentage;
        fillImage.color = c;
    }

    public void SetMaxHitPoints(int maxHitPoints)
    {
        slider.maxValue = maxHitPoints;
    }

    public void SetHitPoints(int hitPoints)
    {
        slider.value = hitPoints;
    }
}
