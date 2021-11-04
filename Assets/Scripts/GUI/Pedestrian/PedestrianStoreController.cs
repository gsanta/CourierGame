
using Pedestrians;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class PedestrianStoreController : MonoBehaviour
    {
        [SerializeField]
        private Pedestrian pedestrianTemplate;
        [SerializeField]
        private GameObject pedestrianContainer;

        private PedestrianStore pedestrianStore;

        [Inject]
        public void Construct(PedestrianStore pedestrianStore)
        {
            this.pedestrianStore = pedestrianStore;
        }

        private void Awake()
        {
            pedestrianStore.SetPedestrianTemplate(pedestrianTemplate);
            pedestrianStore.SetPedestrianContainer(pedestrianContainer);
        }

    }
}
