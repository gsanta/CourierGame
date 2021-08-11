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
    private IWorldState worldState;
    private MarkerHandler markerHandler;
    private Timer timer;

    private HashSet<GameObject> usedPlayerImages = new HashSet<GameObject>();

    [Inject]
    public void Construct(PlayerStore playerStore, IWorldState worldState, Timer timer, MarkerHandler markerHandler)
    {
        this.playerStore = playerStore;
        this.worldState = worldState;
        this.timer = timer;
        this.markerHandler = markerHandler;
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
        if (worldState.IsMeasuring())
        {
            Player player = playerStore.GetActivePlayer();
            if (player)
            {
                slider.SetSliderVal(timer.GetDayPercentageAt(player.ElapsedTime));
                markerHandler.UpdateMarker();
            }
        }
    }
}