using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TimelineController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> playerImages = new List<GameObject>();
    [SerializeField]
    public TimelineSlider slider;

    private PlayerStore playerStore;
    private Timer timer;

    private HashSet<GameObject> usedPlayerImages = new HashSet<GameObject>();

    [Inject]
    public void Construct(PlayerStore playerStore, Timer timer)
    {
        this.playerStore = playerStore;
        this.timer = timer;
    }

    public GameObject GetNextUnusedPlayerImage()
    {
        GameObject image = playerImages.Find(gameObject => !usedPlayerImages.Contains(gameObject));

        if (image)
        {
            usedPlayerImages.Add(image);
            return image;
        }

        return null;
    }

    void Update()
    {
        if (timer.IsDayStarted)
        {
            Debug.Log(timer.GetDayPercentage());
            slider.SetSliderVal(timer.GetDayPercentage());
        }
    }
}