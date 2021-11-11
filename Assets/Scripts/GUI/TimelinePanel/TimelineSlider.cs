using Scenes;
using Stats;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimelineSlider : MonoBehaviour, IReconcilable
{
    private Slider slider;
    private Timer timer;

    [Inject]
    public void Construct(ReconciliationService reconciliationService, Timer timer)
    {
        this.timer = timer;
        reconciliationService.Add(timer, this);
    }

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

    public void Reconcile()
    {
        SetSliderVal(timer.GetDayPercentage());
    }

    private void SetSliderVal(float ratio)
    {
        slider.value = ratio * 100;
    }
}
