using UnityEngine;
using UnityEngine.AI;
using Zenject;
using System;
using Delivery;
using AI;

namespace Bikers
{
    public class Player : MonoBehaviour, IGameObject
    {
        [SerializeField]
        public Transform viewPoint;
        [SerializeField]
        private CharacterController charController;
        [SerializeField]
        public Transform packageHolder;

        public Package package;
        private string courierName;

        private bool isPaused = false;

        private GoapAgent<Player> agent;
        private IGoalProvider goalProvider;

        public NavMeshAgent navMeshAgent;

        public GoapAgent<Player> Agent { get => agent; set => agent = value; }
        public IGoalProvider GoalProvider { get => goalProvider; set => goalProvider = value; }

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public bool Paused
        {
            get => isPaused;
            set
            {
                isPaused = value;
                UpdatePausedState();
            }
        }

        private void LateUpdate()
        {
            if (agent.Active)
            {
                agent.Update();
            }

            Updated?.Invoke(this, EventArgs.Empty);
        }

        private void UpdatePausedState()
        {
            if (isPaused)
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
                agent.Active = false;
            }
            else
            {
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
                agent.Active = true;
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

        public string GetId()
        {
            return agent.AgentId;
        }

        public string GetName()
        {
            return courierName;
        }

        public void SetName(string name)
        {
            this.courierName = name;
        }

        public void SetPackage(Package package)
        {
            this.package = package;
        }

        public Package GetPackage()
        {
            return package;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void CompleteAction()
        {
            agent.CompleteAction();
        }

        public NavMeshAgent GetNavMeshAgent()
        {
            return navMeshAgent;
        }

        public IGoalProvider GetGoalProvider()
        {
            return goalProvider;
        }

        public event EventHandler Updated;

        public class Factory : PlaceholderFactory<UnityEngine.Object, Player>
        {
        }
    }

    public class BikerInfo
    {
        public readonly string home;

        public BikerInfo(string home)
        {
            this.home = home;
        }
    }
}
