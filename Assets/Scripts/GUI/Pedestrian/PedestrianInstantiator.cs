using Pedestrians;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class PedestrianInstantiator : MonoBehaviour, IPedestrianInstantiator
    {
        private PedestrianStore pedestrianStore;
        private PedestrianFactory pedestrianFactory;

        [Inject]
        public void Construct(PedestrianStore pedestrianStore, PedestrianFactory pedestrianFactory)
        {
            this.pedestrianStore = pedestrianStore;
            this.pedestrianFactory = pedestrianFactory;
        }

        private void Awake()
        {
            pedestrianFactory.SetPedestrianInstantiator(this);
        }

        public Pedestrian InstantiatePedestrian()
        {
            return Instantiate(pedestrianStore.GetPedestrianTemplate(), pedestrianStore.GetPedestrianContainer().transform);
        }
    }
}
