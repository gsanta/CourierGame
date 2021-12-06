using GameObjects;
using Enemies;
using Pedestrians;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class CharacterInstantiator : MonoBehaviour, IPedestrianInstantiator, IBikerInstantiator, IEnemyInstantiator
    {
        private PedestrianStore pedestrianStore;
        private PedestrianFactory pedestrianFactory;
        private Pedestrian.Factory gameObjectFactory;
        private GameCharacter.Factory playerInstanceFactory;
        private PlayerStore bikerStore;
        private BikerFactory bikerFactory;
        private EnemiesConfig enemiesConfig;
        private EnemyFactory enemyFactory;

        [Inject]
        public void Construct(PedestrianStore pedestrianStore, PedestrianFactory pedestrianFactory, Pedestrian.Factory gameObjectFactory, GameCharacter.Factory playerInstanceFactory, PlayerStore bikerStore, BikerFactory bikerFactory, EnemiesConfig enemiesConfig, EnemyFactory enemyFactory)
        {
            this.pedestrianStore = pedestrianStore;
            this.pedestrianFactory = pedestrianFactory;
            this.gameObjectFactory = gameObjectFactory;
            this.playerInstanceFactory = playerInstanceFactory;
            this.bikerStore = bikerStore;
            this.bikerFactory = bikerFactory;
            this.enemiesConfig = enemiesConfig;
            this.enemyFactory = enemyFactory;

        }

        private void Awake()
        {
            pedestrianFactory.SetPedestrianInstantiator(this);
            bikerFactory.SetBikerInstantiator(this);
            enemyFactory.SetPedestrianInstantiator(this);
        }

        public Pedestrian InstantiatePedestrian()
        {
            return gameObjectFactory.Create(pedestrianStore.GetPedestrianTemplate());
        }

        public GameCharacter InstantiateBiker()
        {
            //, bikerStore.GetBikerContainer().transform
            return playerInstanceFactory.Create(bikerStore.GetBikerTemplate());
        }

        public MinimapBiker InstantiateMinimapBiker()
        {
            var minimapBiker = bikerStore.GetMinimapBiker();
            return Instantiate(minimapBiker, minimapBiker.transform.parent);
        }

        public GameCharacter InstantiateEnemy()
        {
            return Instantiate(enemiesConfig.enemyTemplate, enemiesConfig.enemyContainer.transform);
        }
    }
}
