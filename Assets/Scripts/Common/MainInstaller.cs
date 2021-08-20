
using AI;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private TimelineController timelineController;
    [SerializeField]
    private StartDayPanel startDayPanel;
    [SerializeField]
    private DeliveryPanel deliveryPanel;
    [SerializeField]
    private InputHandler inputHandler;
    [SerializeField]
    private WorldState worldState;

    [SerializeField]
    private PlayerStore playerStore;
    [SerializeField]
    private PlayerFactory playerFactory;

    [SerializeField]
    private PackageStore packageStore;
    [SerializeField]
    private PackageFactory packageFactory;

    [SerializeField]
    private CourierStore courierStore;
    [SerializeField]
    private CourierFactory courierFactory;
    //[SerializeField]
    //private CourierAgent courierAgnet;

    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromInstance(inputHandler).AsSingle();
        Container.Bind<TimelineController>().FromInstance(timelineController).AsSingle();
        Container.Bind<ITimeProvider>().To<DefaultTimeProvider>().AsSingle();
        Container.Bind<Timer>().FromInstance(timer).AsSingle();
        Container.Bind<TimelineSlider>().FromInstance(timelineController.slider).AsSingle();
        Container.Bind<IWorldState>().To<WorldState>().FromInstance(worldState).AsSingle();
        Container.Bind<ISpawnPointHandler>().To<RandomSpawnPointHandler>().AsTransient();
        Container.Bind<MarkerHandler>().AsSingle();

        Container.Bind<PlayerStore>().FromInstance(playerStore).AsSingle();
        Container.Bind<PlayerFactory>().FromInstance(playerFactory).AsSingle();
        Container.Bind<PlayerSpawner>().AsSingle();
        Container.Bind<PlayerSetup>().AsSingle();
        Container.Bind<PlayerInputComponent>().AsTransient();
        Container.BindFactory<Object, Player, Player.Factory>().FromFactory<PrefabFactory<Player>>();

        Container.Bind<CourierStore>().FromInstance(courierStore).AsSingle();
        Container.Bind<ItemFactory<CourierConfig, CourierAgent>>().To<CourierFactory>().FromInstance(courierFactory).AsSingle();
        Container.Bind<CourierSpawner>().AsSingle();
        Container.Bind<CourierSetup>().AsSingle();
        //Container.Bind<CourierAgent>().FromComponentInNewPrefab(courierAgnet);
        Container.BindFactory<Object, CourierAgent, CourierAgent.Factory>().FromFactory<PrefabFactory<CourierAgent>>();


        Container.Bind<PackageStore>().FromInstance(packageStore).AsSingle();
        Container.Bind<ISpawner<PackageConfig>>().To<PackageSpawner>().AsSingle().NonLazy();
        Container.Bind<ItemFactory<PackageConfig, Package>>().To<PackageFactory>().FromInstance(packageFactory).AsSingle();
        Container.BindFactory<Object, Package, Package.Factory>().FromFactory<PrefabFactory<Package>>();

        Container.Bind<DeliveryStore>().AsSingle();
    }
    override public void Start()
    {
        base.Start();

        PlayerSetup playerSetup = Container.Resolve<PlayerSetup>();
        playerSetup.Setup();
        CourierSetup courierSetup = Container.Resolve<CourierSetup>();
        courierSetup.Setup();

    }
}
