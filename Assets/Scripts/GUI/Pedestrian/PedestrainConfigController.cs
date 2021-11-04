
using Pedestrians;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class PedestrainConfigController : MonoBehaviour
    {
        [SerializeField]
        private int pedestrianCount;

        private PedestrianSpawner pedestrianSpawner;

        [Inject]
        public void Construct(PedestrianSpawner pedestrianSpawner)
        {
            this.pedestrianSpawner = pedestrianSpawner;            
        }

        private void Awake()
        {

            pedestrianSpawner.SetPedestrianCount(pedestrianCount);
        }
    }
}
