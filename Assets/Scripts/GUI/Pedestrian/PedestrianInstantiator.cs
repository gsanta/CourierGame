using Pedestrians;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class PedestrianInstantiator : MonoBehaviour
    {
        private PedestrianStore pedestrianStore;

        [Inject]
        public void Construct(PedestrianStore pedestrianStore)
        {
            this.pedestrianStore = pedestrianStore;
        }

        public Pedestrian InstantiatePedestrian()
        {
            return Instantiate(pedestrianStore.GetPedestrianTemplate(), pedestrianStore.GetPedestrianContainer().transform);
        }
    }
}
