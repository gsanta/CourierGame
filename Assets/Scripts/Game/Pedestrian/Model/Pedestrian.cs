using AI;
using Bikers;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Pedestrians
{
    public class Pedestrian : MonoBehaviour
    {
        public GoapAgent<Pedestrian> agent;
        private AgentFactory agentFactory;
        public NavMeshAgent navMeshAgent;

        public bool walked = false;

        [Inject]
        public void Construct(AgentFactory agentFactory)
        {
            this.agentFactory = agentFactory;
        }

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            var areaIndex = NavMesh.GetAreaFromName("road");
            //navMeshAgent.SetAreaCost(areaIndex, 8);
            agent = agentFactory.CreatePedestrianAgent(this);
            agent.Active = true;
        }

        public void CompleteAction()
        {
            agent.CompleteAction();
        }

        private void LateUpdate()
        {
            if (agent.Active)
            {
                agent.Update();
            }
        }

        public class Factory : PlaceholderFactory<Object, Pedestrian>
        {
        }
    }
}
