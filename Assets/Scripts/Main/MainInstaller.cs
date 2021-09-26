using Delivery;
using Model;
using Road;
using Service;
using UI;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private TimelineController timelineController;
    [SerializeField]
    private PanelManager panelManager;

    [SerializeField]
    private StartDayPanel startDayPanel;
    [SerializeField]
    private DeliveryPanel deliveryPanel;
    [SerializeField]
    private InputHandler inputHandler;

    [SerializeField]
    private PackageStore packageStore;
    [SerializeField]
    private PackageFactory packageFactory;

    [SerializeField]
    private BikerStore bikerStore;
    [SerializeField]
    private BikerFactory bikerFactory;
    [SerializeField]
    private BikerService bikerService;
    [SerializeField]
    private BikerPanel bikerPanel;

    [SerializeField]
    private MainCamera mainCamera;

    [SerializeField]
    private PedestrianSpawner pedestrianSpawner;
    [SerializeField]
    private PedestrianFactory pedestrianFactory;

    [SerializeField]
    private PackageStore2 packageStore2;

    [SerializeField]
    private RoadStore roadStore;

    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromInstance(inputHandler).AsSingle();
        Container.Bind<TimelineController>().FromInstance(timelineController).AsSingle();
        Container.Bind<ITimeProvider>().To<DefaultTimeProvider>().AsSingle();
        Container.Bind<Timer>().FromInstance(timer).AsSingle();
        Container.Bind<TimelineSlider>().FromInstance(timelineController.slider).AsSingle();
        Container.Bind<PanelManager>().FromInstance(panelManager).AsSingle();

        Container.Bind<DayService>().AsSingle();

        Container.Bind<BikerService>().FromInstance(bikerService).AsSingle();
        Container.Bind<BikerStore>().FromInstance(bikerStore).AsSingle();
        Container.Bind<BikerFactory>().FromInstance(bikerFactory).AsSingle();
        Container.Bind<BikerSpawner>().AsSingle();
        Container.Bind<BikerPanel>().FromInstance(bikerPanel).AsSingle();
        Container.Bind<BikerSetup>().AsSingle();
        Container.BindFactory<Object, Biker, Biker.Factory>().FromFactory<PrefabFactory<Biker>>();
        Container.BindFactory<Object, BikerAgentComponent, BikerAgentComponent.Factory>().FromFactory<PrefabFactory<BikerAgentComponent>>();
        Container.BindFactory<Object, BikerPlayComponent, BikerPlayComponent.Factory>().FromFactory<PrefabFactory<BikerPlayComponent>>();
        Container.BindFactory<Object, SteeringComponent, SteeringComponent.Factory>().FromFactory<PrefabFactory<SteeringComponent>>();
        Container.BindFactory<Object, Pedestrian, Pedestrian.Factory>().FromFactory<PrefabFactory<Pedestrian>>();

        Container.Bind<PackageStore>().FromInstance(packageStore).AsSingle();
        Container.Bind<ISpawner<PackageConfig>>().To<PackageSpawner>().AsSingle().NonLazy();
        Container.Bind<ItemFactory<PackageConfig, Package>>().To<PackageFactory>().FromInstance(packageFactory).AsSingle();
        Container.BindFactory<Object, Package, Package.Factory>().FromFactory<PrefabFactory<Package>>();

        Container.Bind<DeliveryPanel>().FromInstance(deliveryPanel).AsSingle();
        Container.Bind<DeliveryStore>().AsSingle();
        Container.Bind<IDeliveryService>().To<DeliveryService>().AsSingle();

        Container.Bind<RoleService>().AsSingle();
        Container.Bind<IEventService>().To<EventService>().AsSingle();

        Container.Bind<MinimapStore>().AsSingle();
        Container.Bind<MinimapPackageProvider>().AsSingle();
        Container.Bind<MinimapPackageConsumer>().AsSingle();

        Container.Bind<MainCamera>().FromInstance(mainCamera).AsSingle();

        Container.Bind<PedestrianSpawner>().FromInstance(pedestrianSpawner).AsSingle();
        Container.Bind<PedestrianStore>().AsSingle();
        Container.Bind<PedestrianFactory>().FromInstance(pedestrianFactory).AsSingle();

        Container.Bind<PackageStore2>().FromInstance(packageStore2).AsSingle();

        Container.Bind<RoadStore>().FromInstance(roadStore).AsSingle();

        // actions
        Container.Bind<RouteAction>().AsTransient();
    }
    override public void Start()
    {
        base.Start();

        BikerSetup bikerSetup = Container.Resolve<BikerSetup>();
        bikerSetup.Setup();
        Container.Resolve<DayService>();
        Container.Resolve<MinimapPackageProvider>();
        Container.Resolve<MinimapPackageConsumer>();

    }
}