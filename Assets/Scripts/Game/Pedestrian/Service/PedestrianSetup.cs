namespace Pedestrians
{
    public class PedestrianSetup
    {
        private readonly PedestrianSpawner pedestrianSpawner;

        public PedestrianSetup(PedestrianSpawner pedestrianSpawner)
        {
            this.pedestrianSpawner = pedestrianSpawner;
        }

        public void Setup()
        {
            this.pedestrianSpawner.Spawn();
        }
    }
}
