using Agents;
using AI;
using Bikers;
using UnityEngine;

namespace Pedestrians
{
    public class PedestrianFactory : ItemFactory<PedestrianConfig, Pedestrian>
    {
        private AgentFactory agentFactory;
        private IPedestrianInstantiator pedestrianInstantiator;
        private PedestrianTargetStore pedestrianTargetStore;

        public PedestrianFactory(AgentFactory agentFactory, PedestrianTargetStore pedestrianTargetStore)
        {
            this.agentFactory = agentFactory;
            this.pedestrianTargetStore = pedestrianTargetStore;
        }

        public void SetPedestrianInstantiator(IPedestrianInstantiator pedestrianInstantiator)
        {
            this.pedestrianInstantiator = pedestrianInstantiator;
        }

        public Pedestrian Create(PedestrianConfig config)
        {
            Pedestrian pedestrian = pedestrianInstantiator.InstantiatePedestrian();
            pedestrian.pedestrianInfo = CreatePedestrianInfo();
            pedestrian.agent = agentFactory.CreatePedestrianAgent(pedestrian);
            pedestrian.GoalProvider = new PedestrianGoalProvider(pedestrian, pedestrianTargetStore);

            Transform child = config.spawnPoint.transform;
            pedestrian.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            pedestrian.transform.position = child.position;

            return pedestrian;
        }

        private PedestrianInfo CreatePedestrianInfo()
        {
            var num = Random.Range(1, 5);
            return new PedestrianInfo($"House {num}");
        }

        public void InitializeObj(Pedestrian pedestrian)
        {
            pedestrian.transform.position = pedestrian.GetComponent<WaypointNavigator>().currentWaypoint.transform.position;
        }
    }
}
