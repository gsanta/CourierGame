using AI;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemies {
    public class Enemy : MonoBehaviour, IGameObject
    {
        public GoapAgent<Enemy> agent;
        public NavMeshAgent navMeshAgent;
        private IGoalProvider goalProvider;

        public IGoalProvider GoalProvider { get => goalProvider; set => goalProvider = value; }
        public GoapAgent<Enemy> Agent { get => agent; set => agent = value; }

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

        public NavMeshAgent GetNavMeshAgent()
        {
            return navMeshAgent;
        }

        public IGoalProvider GetGoalProvider()
        {
            return goalProvider;
        }

        public class Factory : PlaceholderFactory<Object, Enemy>
        {
        }
    }
}
