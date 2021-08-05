using System.Collections.Generic;
using UnityEngine;

public class TimelineController : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerImages = new List<GameObject>();
    [SerializeField] private TimelineSlider slider;
    [HideInInspector] public PlayerService playerService;

    private MarkerHandler markerHandler;

    private HashSet<GameObject> usedPlayerImages = new HashSet<GameObject>();

    void Start()
    {
        markerHandler = new MarkerHandler(playerService, slider);
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
        if (WorldProperties.Instance().isMeasuring)
        {
            Player player = playerService.GetActivePlayer();
            if (player)
            {
                slider.SetSliderVal(player.Timer.GetDayPercentage());
                markerHandler.UpdateMarker();
            }
        }
    }
}

//public class Timeline
//{
//    private ITimeProvider timeProvider;
//    private DateTime startTime;
//    private DateTime currTime;
//    private bool isMeasuring = false;
//    private bool isDayPassed = false;

//    public Timeline(ITimeProvider _timeProvider)
//    {
//        timeProvider = _timeProvider;
//    }

//    public float DaySeconds { get; set; }
//    public bool IsMeasuring { get => isMeasuring; }
//    public bool IsDayPassed { get => isDayPassed; }

//    public void StartMeasure()
//    {
//        startTime = timeProvider.UtcNow();
//        isMeasuring = true;
//        isDayPassed = false;
//    }

//    public void UpdateTime()
//    {
//        currTime = timeProvider.UtcNow();

//        if (GetDayPercentage() >= 1)
//        {
//            currTime = startTime.AddSeconds(DaySeconds);
//            isDayPassed = true;

//            OnDayPassed?.Invoke(this, EventArgs.Empty);
//        }
//    }

//    public void ResetMeasure()
//    {
//        isMeasuring = false;
//        isDayPassed = false;
//    }


//    public float GetDayPercentage()
//    {
//        return GetTimeInSec() / DaySeconds;
//    }

//    public int GetTimeInSec()
//    {
//        return (currTime - startTime).Seconds;
//    }

//    public event EventHandler OnDayPassed;
//}