
using UnityEngine;

public class MarkerHandler
{
    private PlayerStore playerStore;
    private TimelineSlider timelineSlider;

    public MarkerHandler(PlayerStore playerStore, TimelineSlider timelineSlider)
    {
        this.playerStore = playerStore;
        this.timelineSlider = timelineSlider;
    }
    
    public void UpdateMarker()
    {
        if (playerStore.GetActivePlayer())
        {
            Player player = playerStore.GetActivePlayer();

            float x = timelineSlider.GetWidth() * player.Timer.GetDayPercentage();
            RectTransform rectTransform = player.TimelineImage.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector3(x, rectTransform.anchoredPosition.y);
        }
    }
}
