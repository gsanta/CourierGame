using UnityEngine;

public class DependencyResolver : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TimelineController timelineController;
    [SerializeField] private StartDayPanel startDayPanel;
    [SerializeField] private DeliveryPanel deliveryPanel;
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private WorldState worldState;

    [SerializeField]
    private PlayerFactory playerFactory;
    [SerializeField]
    private PlayerPool playerPool;
    [SerializeField]
    private PlayerSpawner playerSpawner;
    private PlayerStore playerStore;
    private PlayerSetup playerSetup;

    [SerializeField]
    private PackageFactory packageFactory;
    private PackageStore packageStore;
    private PackageSetup packageSetup;

    private DeliveryStore deliveryService;
    private ITimeProvider timeProvider;

    void Awake()
    {
        packageStore = new PackageStore();
        deliveryService = new DeliveryStore(packageStore);
        timeProvider = new DefaultTimeProvider();
        packageSetup = new PackageSetup(packageStore, packageFactory);

        playerStore = new PlayerStore();
        playerSetup = new PlayerSetup(playerPool, playerSpawner, playerFactory, playerStore);

        packageFactory.deliveryService = deliveryService;
        packageFactory.playerFactory = playerFactory;
        deliveryPanel.SetDependencies(deliveryService, packageStore);
        playerFactory.SetDependencies(packageStore, deliveryService, timelineController, timeProvider, worldState, playerStore, inputHandler);
        timelineController.SetDependencies(playerStore, worldState);
        startDayPanel.SetDependencies(worldState);
    }

    void Start()
    {
        packageSetup.Setup();
        playerSetup.Setup();
    }
}
