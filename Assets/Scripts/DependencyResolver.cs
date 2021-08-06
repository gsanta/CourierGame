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
    private SpawnPointHandler playerSpawnPointHandler;
    private PlayerStore playerStore;
    private PlayerSetup playerSetup;

    [SerializeField]
    private PackageFactory packageFactory;
    private PackageStore packageStore;

    private DeliveryStore deliveryService;
    private ITimeProvider timeProvider;

    void Awake()
    {
        packageStore = new PackageStore();
        deliveryService = new DeliveryStore(packageStore);
        timeProvider = new DefaultTimeProvider();

        playerStore = new PlayerStore();
        playerSetup = new PlayerSetup(playerPool, playerSpawnPointHandler, playerFactory, playerStore);

        packageFactory.deliveryService = deliveryService;
        packageFactory.playerFactory = playerFactory;
        deliveryPanel.SetDependencies(deliveryService, packageStore);
        playerFactory.SetDependencies(packageStore, deliveryService, timelineController, timeProvider, worldState, playerStore, inputHandler);
        timelineController.SetDependencies(playerStore, worldState);
        startDayPanel.SetDependencies(worldState);
        playerPool.SetDependencies(playerSpawnPointHandler);

        playerSetup.Setup();
    }
}
