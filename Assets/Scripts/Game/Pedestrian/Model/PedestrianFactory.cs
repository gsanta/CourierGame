using AI;
using Bikers;
using UnityEngine;

namespace Pedestrians
{
    public class PedestrianFactory : ItemFactory<PedestrianConfig, Pedestrian>
    {
        private AgentFactory agentFactory;
        private IPedestrianInstantiator pedestrianInstantiator;

        public PedestrianFactory(AgentFactory agentFactory, IPedestrianInstantiator pedestrianInstantiator)
        {
            this.agentFactory = agentFactory;
            this.pedestrianInstantiator = pedestrianInstantiator;
        }

        public Pedestrian Create(PedestrianConfig config)
        {
            Pedestrian pedestrian = pedestrianInstantiator.InstantiatePedestrian();
            pedestrian.Construct(agentFactory);
            Transform child = config.spawnPoint.transform;
            pedestrian.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            pedestrian.transform.position = child.position;

            return pedestrian;
        }

        public void InitializeObj(Pedestrian pedestrian)
        {
            pedestrian.transform.position = pedestrian.GetComponent<WaypointNavigator>().currentWaypoint.transform.position;
        }
    }
}
