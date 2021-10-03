﻿using AI;
using Bikers;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Pedestrians
{
    public class Pedestrian : MonoBehaviour
    {
        public float seeAhead = 5f;
        public GoapAgent<Pedestrian> agent;
        private AgentFactory agentFactory;
        public NavMeshAgent navMeshAgent;

        [Inject]
        public void Construct(AgentFactory agentFactory)
        {
            this.agentFactory = agentFactory;
        }

        private void Update()
        {
            Debug.Log("pedestrian update");
        }

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
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
