using UnityEngine;
using UnityEngine.UI;

public class DayMeasurerSlider : MonoBehaviour
{
    [HideInInspector] public DayMeasurer dayMeasurer;
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = 100;
        slider.value = 0;
    }

    void Update()
    {
        if (dayMeasurer != null)
        {
            slider.value = dayMeasurer.GetDayPercentage() * 100;
        }
    }
}
