using System;
using UnityEngine;
using UnityEngine.UI;

public class TimelineSlider : MonoBehaviour
{
    private Slider slider;

    public int GetWidth()
    {
        var rectTransform = GetComponent<RectTransform>();
        float width = Math.Abs(rectTransform.rect.width);
        return (int) width;
    }

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = 100;
        slider.value = 0;
    }

    public void SetSliderVal(float ratio)
    {
        slider.value = ratio * 100;
    }
}
