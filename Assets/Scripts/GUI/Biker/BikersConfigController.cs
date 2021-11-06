using UnityEngine;
using Zenject;

namespace Bikers
{
    public class BikersConfigController : MonoBehaviour
    {
        [SerializeField]
        private int bikerCount;
        private BikersConfig bikersConfig;

        [Inject]
        public void Construct(BikersConfig bikersConfig)
        {
            this.bikersConfig = bikersConfig;
        }

        private void Awake()
        {
            bikersConfig.BikerCount = bikerCount;
        }
    }
}
