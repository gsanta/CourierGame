using Bikers;
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
        private BikerStore bikerStore;
        private BikerFactory bikerFactory;
        private EnemiesConfig enemiesConfig;
        private EnemyFactory enemyFactory;

        [Inject]
        public void Construct(PedestrianStore pedestrianStore, PedestrianFactory pedestrianFactory, Pedestrian.Factory gameObjectFactory, BikerStore bikerStore, BikerFactory bikerFactory, EnemiesConfig enemiesConfig, EnemyFactory enemyFactory)
        {
            this.pedestrianStore = pedestrianStore;
            this.pedestrianFactory = pedestrianFactory;
            this.gameObjectFactory = gameObjectFactory;
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

        public Biker InstantiateBiker()
        {
            return Instantiate(bikerStore.GetBikerTemplate(), bikerStore.GetBikerContainer().transform);
        }

        public MinimapBiker InstantiateMinimapBiker()
        {
            var minimapBiker = bikerStore.GetMinimapBiker();
            return Instantiate(minimapBiker, minimapBiker.transform.parent);
        }

        public Enemy InstantiateEnemy()
        {
            return Instantiate(enemiesConfig.enemyTemplate, enemiesConfig.enemyContainer.transform);
        }
    }
}
