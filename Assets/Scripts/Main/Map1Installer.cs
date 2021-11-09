using Agents;
using Bikers;
using Buildings;
using Delivery;
using Enemies;
using Controls;
using Minimap;
using Pedestrians;
using Route;
using Scenes;
using Stats;
using UnityEngine;
using Zenject;
using AI;
using System.Collections.Generic;

public class Map1Installer : MonoInstaller, ISceneSetup
{
    [SerializeField]
    private CameraHandler mainCamera;
    [SerializeField]
    private SceneInitializer sceneInitializer;
    [SerializeField]
    private GameObject roadSystem;
    [SerializeField]
    private GameObject staticObjects;
    [SerializeField]
    private GameObject characters;

    public override void InstallBindings()
    {
        Container.Bind<DayService>().AsSingle();

        Container.Bind<BikerSpawner>().AsSingle();
        Container.Bind<BikerSetup>().AsSingle();
        Container.BindFactory<Object, Biker, Biker.Factory>().FromFactory<PrefabFactory<Biker>>();
        Container.BindFactory<Object, Pedestrian, Pedestrian.Factory>().FromFactory<PrefabFactory<Pedestrian>>();

        Container.Bind<ISpawner<PackageConfig>>().To<PackageSpawner>().AsSingle().NonLazy();
        Container.BindFactory<Object, Package, Package.Factory>().FromFactory<PrefabFactory<Package>>();

        Container.Bind<DeliveryStore>().AsSingle();

        Container.Bind<MinimapStore>().AsSingle();
        Container.Bind<MinimapPackageProvider>().AsSingle();
        Container.Bind<MinimapPackageConsumer>().AsSingle();

        Container.Bind<CameraHandler>().FromInstance(mainCamera).AsSingle();

        //characters
        Container.Bind<PedestrianSetup>().AsSingle();
        Container.Bind<PedestrianConfigHandler>().FromInstance(characters.GetComponent<PedestrianConfigHandler>()).AsSingle();
        Container.Bind<BikersConfigHandler>().FromInstance(characters.GetComponent<BikersConfigHandler>()).AsSingle();
        Container.Bind<EnemiesConfigHandler>().FromInstance(characters.GetComponent<EnemiesConfigHandler>()).AsSingle();

        // static objects

        // road system
        Container.Bind<RouteFacade>().AsSingle();
        Container.Bind<RouteSetup>().AsSingle().NonLazy();
        
        Container.Bind<SceneInitializer>().FromInstance(sceneInitializer).AsSingle().NonLazy();

        // actions
        Container.Bind<PickUpPackageAction>().AsSingle().NonLazy();
        Container.Bind<DeliverPackageAction>().AsSingle().NonLazy();
        Container.Bind<ReservePackageAction>().AsSingle().NonLazy();
        Container.Bind<WalkAction<Pedestrian>>().AsSingle().NonLazy();
        Container.Bind<WalkAction<Enemy>>().AsSingle().NonLazy();
        Container.Bind<GoHomeAction>().AsSingle().NonLazy();
        Container.Bind<PedestrianActionCreator>().AsSingle();
        Container.Bind<EnemyActionCreator>().AsSingle();

        Container.Bind<PathCache>().AsSingle().NonLazy();
        Container.Bind<BuildingConfigHandler>().AsSingle();

        Container.Bind<EnemySetup>().AsSingle();
        //Container.Bind<EnemyInstantiator>().FromInstance(enemyInstantiator).AsSingle();

        Container.ParentContainers[0].Resolve<SceneInitializer>().SetSceneSetup(this);

        AgentFactory agentFactory = Container.Resolve<AgentFactory>();
        agentFactory.AddBikerAction(Container.Resolve<PickUpPackageAction>());
        agentFactory.AddBikerAction(Container.Resolve<DeliverPackageAction>());
        agentFactory.AddBikerAction(Container.Resolve<ReservePackageAction>());
    }

    public void SetupScene()
    {
        WalkTargetStore walkTargetStore = Container.Resolve<WalkTargetStore>();

        Container.Resolve<DayService>();
        Container.Resolve<MinimapPackageProvider>();
        Container.Resolve<MinimapPackageConsumer>();

        PathCache pathCache = Container.Resolve<PathCache>();
        pathCache.SetWalkTargetStore(walkTargetStore);
        pathCache.Init();

        RouteFacade routeFacade = Container.Resolve<RouteFacade>();
        ActionStore actionStore = Container.Resolve<ActionStore>();
        BuildingStore buildingStore = Container.Resolve<BuildingStore>();

        PedestrianActionCreator pedestrianActionCreator = Container.Resolve<PedestrianActionCreator>();
        pedestrianActionCreator.SetActions(new List<GoapAction<Pedestrian>>() {
            new WalkAction<Pedestrian>(new AIStateName[] { }, new AIStateName[] { AIStateName.DESTINATION_REACHED }, routeFacade, walkTargetStore, pathCache),
            new GoHomeAction(routeFacade, pathCache, buildingStore)
        });

        EnemyActionCreator enemyActionCreator = Container.Resolve<EnemyActionCreator>();
        enemyActionCreator.SetActions(new List<GoapAction<Enemy>>()
        {
            new WalkAction<Enemy>(new AIStateName[] { }, new AIStateName[] { AIStateName.DESTINATION_REACHED }, routeFacade, walkTargetStore, pathCache),
        });

        actionStore.SetPedestrianActionCreator(pedestrianActionCreator);
        actionStore.SetEnemyActionCreator(enemyActionCreator);

        actionStore.SetEnemyWalkAction(new WalkAction<Enemy>(new AIStateName[] { }, new AIStateName[] { AIStateName.DESTINATION_REACHED }, routeFacade, walkTargetStore, pathCache));
        actionStore.SetPedestrianWalkAction(new WalkAction<Pedestrian>(new AIStateName[] { }, new AIStateName[] { AIStateName.DESTINATION_REACHED }, routeFacade, walkTargetStore, pathCache));

        BikerSetup bikerSetup = Container.Resolve<BikerSetup>();
        bikerSetup.Setup();
        PedestrianSetup pedestrianSetup = Container.Resolve<PedestrianSetup>();
        pedestrianSetup.Setup();
        EnemySetup enemySetup = Container.Resolve<EnemySetup>();
        enemySetup.Setup();
    }
}
