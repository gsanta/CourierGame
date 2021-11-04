using Pedestrians;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class PedestrianInstantiator : MonoBehaviour, IPedestrianInstantiator
    {
        private PedestrianStore pedestrianStore;
        private PedestrianFactory pedestrianFactory;
        private Pedestrian.Factory gameObjectFactory;

        [Inject]
        public void Construct(PedestrianStore pedestrianStore, PedestrianFactory pedestrianFactory, Pedestrian.Factory gameObjectFactory)
        {
            this.pedestrianStore = pedestrianStore;
            this.pedestrianFactory = pedestrianFactory;
            this.gameObjectFactory = gameObjectFactory;
        }

        private void Awake()
        {
            pedestrianFactory.SetPedestrianInstantiator(this);
        }

        public Pedestrian InstantiatePedestrian()
        {
            return gameObjectFactory.Create(pedestrianStore.GetPedestrianTemplate());
            //return Instantiate(pedestrianStore.GetPedestrianTemplate(), pedestrianStore.GetPedestrianContainer().transform);
        }
    }
}
