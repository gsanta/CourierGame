using UnityEngine;

public class DependencyResolver : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private DeliveryPackageController deliveryPackageController;
    [SerializeField] private DayMeasurerBehaviour dayMeasurerBehaviour;
    [SerializeField] private DayMeasurerSlider dayMeasurerSlider;
    [SerializeField] private StartDayPanel startDayPanel;

    private PlayerService playerService;
    private DeliveryService deliveryService;
    private ITimeProvider timeProvider;
    private DayMeasurer dayMeasurer;

    void Awake()
    {
        playerService = new PlayerService();
        deliveryService = new DeliveryService();
        timeProvider = new DefaultTimeProvider();
        dayMeasurer = new DayMeasurer(timeProvider);
        playerController.playerService = playerService;
        deliveryPackageController.deliveryService = deliveryService;
        dayMeasurerBehaviour.dayMeasurer = dayMeasurer;
        dayMeasurerSlider.dayMeasurer = dayMeasurer;
        startDayPanel.dayMeasurer = dayMeasurer;

    }
}
