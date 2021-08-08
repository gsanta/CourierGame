
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField]
    private Player player;
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
    private PlayerFactory playerFactory;
    [SerializeField]
    private PlayerPool playerPool;
    [SerializeField]
    private PlayerSpawner playerSpawner;

    [SerializeField]
    private PackageFactory packageFactory;
    [SerializeField]
    private PackageSpawner packageSpawner;

    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromInstance(inputHandler).AsSingle();
        Container.Bind<TimelineController>().FromInstance(timelineController).AsSingle();
        Container.Bind<ITimeProvider>().To<DefaultTimeProvider>().AsSingle();
        Container.Bind<IWorldState>().To<WorldState>().FromInstance(worldState).AsSingle();
        Container.Bind<ISpawnPointHandler>().To<RandomSpawnPointHandler>().AsTransient();

        Container.Bind<PlayerStore>().AsSingle();
        Container.Bind<PlayerPool>().FromInstance(playerPool).AsSingle();
        Container.Bind<PlayerSpawner>().FromInstance(playerSpawner).AsSingle();
        Container.Bind<PlayerFactory>().FromInstance(playerFactory).AsSingle();
        Container.Bind<PlayerSetup>().AsSingle();
        Container.Bind<PlayerInputComponent>().AsTransient();
        Container.BindFactory<Object, Player, Player.Factory>().FromFactory<PrefabFactory<Player>>();

        Container.Bind<PackageStore>().AsSingle();
        Container.Bind<PackageFactory>().FromInstance(packageFactory).AsSingle();
        Container.Bind<PackageSpawner>().FromInstance(packageSpawner).AsSingle();
        Container.Bind<PackageSetup>().AsSingle();
        Container.BindFactory<Object, Package, Package.Factory>().FromFactory<PrefabFactory<Package>>();

        Container.Bind<DeliveryStore>().AsSingle();
    }

    override public void Start()
    {
        base.Start();

        PlayerSetup playerSetup = Container.Resolve<PlayerSetup>();
        playerSetup.Setup();

        PackageSetup packageSetup = Container.Resolve<PackageSetup>();
        packageSetup.Setup();
    }
}
