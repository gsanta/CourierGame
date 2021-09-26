using Service;

namespace Bikers
{
    public class BikerSetup

    {
        private readonly BikerSpawner courierSpawner;

        public BikerSetup(BikerSpawner courierSpawner)
        {
            this.courierSpawner = courierSpawner;
        }

        public void Setup()
        {
            this.courierSpawner.Spawn();
        }
    }
}
