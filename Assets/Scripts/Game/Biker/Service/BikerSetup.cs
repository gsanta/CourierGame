
namespace Bikers
{
    public class BikerSetup
    {
        private readonly BikerSpawner bikerSpawner;

        public BikerSetup(BikerSpawner bikerSpawner)
        {
            this.bikerSpawner = bikerSpawner;
        }

        public void Setup()
        {
            this.bikerSpawner.Spawn();
        }
    }
}
