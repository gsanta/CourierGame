
using Pedestrians;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class PedestrianConfigHandler : MonoBehaviour
    {
        [SerializeField]
        private int pedestrianCount;
        [SerializeField]
        private Pedestrian pedestrianTemplate;
        [SerializeField]
        private GameObject pedestrianContainer;

        private PedestrianSpawner pedestrianSpawner;
        private PedestrianStore pedestrianStore;

        [Inject]
        public void Construct(PedestrianSpawner pedestrianSpawner, PedestrianStore pedestrianStore)
        {
            this.pedestrianSpawner = pedestrianSpawner;
            this.pedestrianStore = pedestrianStore;
        }

        private void Awake()
        {
            pedestrianSpawner.SetPedestrianCount(pedestrianCount);
            pedestrianStore.SetPedestrianTemplate(pedestrianTemplate);
            pedestrianStore.SetPedestrianContainer(pedestrianContainer);
        }
    }
}
