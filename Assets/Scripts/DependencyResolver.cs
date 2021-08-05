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

    private DeliveryService deliveryService;
    private ITimeProvider timeProvider;

    void Awake()
    {
        deliveryService = new DeliveryService();
        timeProvider = new DefaultTimeProvider();
        playerService.deliveryService = deliveryService;
        playerService.deliveryPackageController = deliveryPackageController;
        playerService.inputHandler = inputHandler;
        playerService.timelineController = timelineController;
        playerService.timeProvider = timeProvider;
        deliveryPackageController.deliveryService = deliveryService;
        deliveryPackageController.playerService = playerService;
        timelineController.playerService = playerService;
        deliveryPanel.deliveryService = deliveryService;
    }
}
