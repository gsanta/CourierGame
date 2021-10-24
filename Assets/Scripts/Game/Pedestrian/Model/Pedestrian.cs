using AI;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Pedestrians
{
    public class Pedestrian : MonoBehaviour, IGameObject
    {
        public GoapAgent<Pedestrian> agent;
        public NavMeshAgent navMeshAgent;
        public PedestrianInfo pedestrianInfo;
        private IGoalProvider goalProvider;
        public bool walked = false;

        public IGoalProvider GoalProvider { get => goalProvider; set => goalProvider = value; }
        public GoapAgent<Pedestrian> Agent { get => agent; set => agent = value; }


        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
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

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public MonoBehaviour GetMonoBehaviour()
        {
            return this;
        }

        public class Factory : PlaceholderFactory<Object, Pedestrian>
        {
        }
    }

    public class PedestrianInfo
    {
        public readonly string home;

        public PedestrianInfo(string home)
        {
            this.home = home;
        }
    }
}
