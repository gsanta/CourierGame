using UnityEngine;

public class DependencyResolver : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private DeliveryPackageController deliveryPackageController;
    [SerializeField] private TimelineController timelineController;
    [SerializeField] private TimelineSlider timelineSlider;
    [SerializeField] private StartDayPanel startDayPanel;
    [SerializeField] private DeliveryPanel deliveryPanel;
    [SerializeField] private PlayerService playerService;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private WorldState worldState;

    private DeliveryService deliveryService;
    private ITimeProvider timeProvider;

    void Awake()
    {
        deliveryService = new DeliveryService();
        timeProvider = new DefaultTimeProvider();
        deliveryPackageController.deliveryService = deliveryService;
        deliveryPackageController.playerService = playerService;
        deliveryPanel.deliveryService = deliveryService;
        playerService.SetDependencies(deliveryPackageController, deliveryService, inputHandler, timelineController, timeProvider, worldState);
        timelineController.SetDependencies(playerService, worldState);
        startDayPanel.SetDependencies(worldState);
    }
}
