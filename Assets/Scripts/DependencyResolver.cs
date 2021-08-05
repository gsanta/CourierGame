using UnityEngine;

public class DependencyResolver : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private DeliveryPackageController deliveryPackageController;
    [SerializeField] private DayMeasurerBehaviour dayMeasurerBehaviour;
    [SerializeField] private DayMeasurerSlider dayMeasurerSlider;
    [SerializeField] private StartDayPanel startDayPanel;
    [SerializeField] private DeliveryPanel deliveryPanel;
    [SerializeField] private PlayerService playerService;
    [SerializeField] private InputHandler inputHandler;

    private DeliveryService deliveryService;
    private ITimeProvider timeProvider;
    private DayMeasurer dayMeasurer;

    void Awake()
    {
        deliveryService = new DeliveryService();
        timeProvider = new DefaultTimeProvider();
        dayMeasurer = new DayMeasurer(timeProvider);
        playerService.deliveryService = deliveryService;
        playerService.deliveryPackageController = deliveryPackageController;
        playerService.inputHandler = inputHandler;
        player.playerService = playerService;
        player.packageService = deliveryPackageController;
        player.deliveryService = deliveryService;
        deliveryPackageController.deliveryService = deliveryService;
        deliveryPackageController.playerService = playerService;
        dayMeasurerBehaviour.dayMeasurer = dayMeasurer;
        dayMeasurerSlider.dayMeasurer = dayMeasurer;
        startDayPanel.dayMeasurer = dayMeasurer;
        deliveryPanel.deliveryService = deliveryService;
    }
}
