
using AI;
using Domain;
using Service;
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
    [SerializeField]
    private CourierService courierService;

    [SerializeField]
    private MainCamera mainCamera;

    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromInstance(inputHandler).AsSingle();
        Container.Bind<TimelineController>().FromInstance(timelineController).AsSingle();
        Container.Bind<ITimeProvider>().To<DefaultTimeProvider>().AsSingle();
        Container.Bind<Timer>().FromInstance(timer).AsSingle();
        Container.Bind<TimelineSlider>().FromInstance(timelineController.slider).AsSingle();
        Container.Bind<ISpawnPointHandler>().To<RandomSpawnPointHandler>().AsTransient();

        Container.Bind<PlayerStore>().FromInstance(playerStore).AsSingle();
        Container.Bind<PlayerFactory>().FromInstance(playerFactory).AsSingle();
        Container.Bind<PlayerSpawner>().AsSingle();
        Container.Bind<PlayerSetup>().AsSingle();
        Container.Bind<PlayerInputComponent>().AsTransient();
        Container.BindFactory<Object, Player, Player.Factory>().FromFactory<PrefabFactory<Player>>();

        Container.Bind<CourierService>().FromInstance(courierService).AsSingle();
        Container.Bind<CourierStore>().FromInstance(courierStore).AsSingle();
        Container.Bind<CourierFactory>().FromInstance(courierFactory).AsSingle();
        Container.Bind<CourierSpawner>().AsSingle();
        Container.Bind<CourierSetup>().AsSingle();
        Container.Bind<ICourierCallbacks>().To<CourierCallbacksImpl>().AsSingle();
        //Container.Bind<CourierAgent>().FromComponentInNewPrefab(courierAgnet);
        Container.BindFactory<Object, Courier, Courier.Factory>().FromFactory<PrefabFactory<Courier>>();


        Container.Bind<PackageStore>().FromInstance(packageStore).AsSingle();
        Container.Bind<ISpawner<PackageConfig>>().To<PackageSpawner>().AsSingle().NonLazy();
        Container.Bind<ItemFactory<PackageConfig, Package>>().To<PackageFactory>().FromInstance(packageFactory).AsSingle();
        Container.BindFactory<Object, Package, Package.Factory>().FromFactory<PrefabFactory<Package>>();

        Container.Bind<DeliveryStore>().AsSingle();

        Container.Bind<MainCamera>().FromInstance(mainCamera).AsSingle();
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
