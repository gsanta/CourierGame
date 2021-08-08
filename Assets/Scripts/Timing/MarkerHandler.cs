
using UnityEngine;

public class MarkerHandler
{
    private PlayerStore playerStore;
    private TimelineSlider timelineSlider;
    private Timer timer;

    public MarkerHandler(PlayerStore playerStore, TimelineSlider timelineSlider, Timer timer)
    {
        this.playerStore = playerStore;
        this.timelineSlider = timelineSlider;
        this.timer = timer;
    }
    
    public void UpdateMarker()
    {
        if (playerStore.GetActivePlayer())
        {
            Player player = playerStore.GetActivePlayer();

            float x = timelineSlider.GetWidth() * timer.GetDayPercentageAt(player.ElapsedTime);
            RectTransform rectTransform = player.TimelineImage.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector3(x, rectTransform.anchoredPosition.y);
        }
    }
}
