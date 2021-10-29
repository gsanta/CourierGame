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
        private WalkTargetStore walkTargetStore;

        public PedestrianFactory(AgentFactory agentFactory, WalkTargetStore walkTargetStore)
        {
            this.agentFactory = agentFactory;
            this.walkTargetStore = walkTargetStore;
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
            pedestrian.GoalProvider = new PedestrianGoalProvider(pedestrian, walkTargetStore);

            Transform child = config.spawnPoint.transform;
            pedestrian.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
            pedestrian.transform.position = child.position;

            return pedestrian;
        }

        private PedestrianInfo CreatePedestrianInfo()
        {
            var num = Random.Range(1, 4);
            return new PedestrianInfo($"Building {num}");
        }

        public void InitializeObj(Pedestrian pedestrian)
        {
            pedestrian.transform.position = pedestrian.GetComponent<WaypointNavigator>().currentWaypoint.transform.position;
        }
    }
}
