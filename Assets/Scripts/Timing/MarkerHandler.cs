
using UnityEngine;

public class MarkerHandler
{
    private PlayerService playerService;
    private TimelineSlider timelineSlider;

    public MarkerHandler(PlayerService playerService, TimelineSlider timelineSlider)
    {
        this.playerService = playerService;
        this.timelineSlider = timelineSlider;
    }
    
    public void UpdateMarker()
    {
        if (playerService.GetActivePlayer())
        {
            Player player = playerService.GetActivePlayer();

            float x = timelineSlider.GetWidth() * player.Timer.GetDayPercentage();
            RectTransform rectTransform = player.TimelineImage.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector3(x, rectTransform.anchoredPosition.y);
        }
    }
}
