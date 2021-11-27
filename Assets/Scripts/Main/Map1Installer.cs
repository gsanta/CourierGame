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
    private GameObject scripts;
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
        Container.BindFactory<Object, Player, Player.Factory>().FromFactory<PrefabFactory<Player>>();
        Container.BindFactory<Object, Pedestrian, Pedestrian.Factory>().FromFactory<PrefabFactory<Pedestrian>>();

        Container.Bind<ISpawner<PackageConfig>>().To<PackageSpawner>().AsSingle().NonLazy();
        Container.BindFactory<Object, Package, Package.Factory>().FromFactory<PrefabFactory<Package>>();

        Container.Bind<DeliveryStore>().AsSingle();

        Container.Bind<MinimapStore>().AsSingle();
        Container.Bind<MinimapPackageConsumer>().AsSingle();
        Container.Bind<RoadConfig>().FromInstance(roadSystem.GetComponent<RoadConfig>()).AsSingle();

        //characters
        Container.Bind<PedestrianSetup>().AsSingle();
        Container.Bind<PedestrianConfigHandler>().FromInstance(characters.GetComponent<PedestrianConfigHandler>()).AsSingle();
        Container.Bind<BikersConfigHandler>().FromInstance(characters.GetComponent<BikersConfigHandler>()).AsSingle();
        Container.Bind<EnemiesConfigHandler>().FromInstance(characters.GetComponent<EnemiesConfigHandler>()).AsSingle();

        Container.Bind<ISceneInitializer>().FromInstance(scripts.GetComponent<ISceneInitializer>()).AsSingle().NonLazy();

        // actions
        Container.Bind<ReservePackageAction>().AsSingle().NonLazy();
        //Container.Bind<WalkAction<Pedestrian>>().AsSingle().NonLazy();
        //Container.Bind<WalkAction<Enemy>>().AsSingle().NonLazy();
        Container.Bind<GoHomeAction>().AsSingle().NonLazy();
        Container.Bind<PedestrianActionCreator>().AsSingle();
        Container.Bind<EnemyActionCreator>().AsSingle();

        Container.Bind<PathCache>().AsSingle().NonLazy();
        Container.Bind<BuildingConfigHandler>().AsSingle();

        Container.Bind<EnemySetup>().AsSingle();
        //Container.Bind<EnemyInstantiator>().FromInstance(enemyInstantiator).AsSingle();

        Container.Resolve<ISceneInitializer>().SetSceneSetup(this);

        AgentFactory agentFactory = Container.Resolve<AgentFactory>();
        //agentFactory.AddBikerAction(Container.Resolve<PickUpPackageAction>());
        //agentFactory.AddBikerAction(Container.Resolve<DeliverPackageAction>());
        //agentFactory.AddBikerAction(Container.Resolve<ReservePackageAction>());
    }

    public void SetupScene()
    {
        SetupRoad();
        WalkTargetStore walkTargetStore = Container.Resolve<WalkTargetStore>();

        Container.Resolve<DayService>();
        Container.Resolve<MinimapPackageConsumer>();

        PathCache pathCache = Container.Resolve<PathCache>();
        pathCache.SetWalkTargetStore(walkTargetStore);
        pathCache.Init();

        ActionStore actionStore = Container.Resolve<ActionStore>();

        PedestrianActionCreator pedestrianActionCreator = Container.Resolve<PedestrianActionCreator>();
        EnemyActionCreator enemyActionCreator = Container.Resolve<EnemyActionCreator>();
        actionStore.SetPedestrianActionCreator(pedestrianActionCreator);
        actionStore.SetEnemyActionCreator(enemyActionCreator);

        BikerSetup bikerSetup = Container.Resolve<BikerSetup>();
        bikerSetup.Setup();
        PedestrianSetup pedestrianSetup = Container.Resolve<PedestrianSetup>();
        pedestrianSetup.Setup();
        EnemySetup enemySetup = Container.Resolve<EnemySetup>();
        enemySetup.Setup();

    }

    private void SetupRoad()
    {
        RoadConfig roadConfig = Container.Resolve<RoadConfig>();
        RoadStore roadStore = Container.Resolve<RoadStore>();

        DirectedGraph<Waypoint, object> graph = new DirectedGraph<Waypoint, object>();
        WaypointGraphBuilder builder = new WaypointGraphBuilder();
        builder.BuildGraph(roadConfig.Waypoints, graph);

        Road<Waypoint> road = new Road<Waypoint>(graph, new WaypointScorer());

        roadStore.AddRoad("Map1", road);
    }
}
