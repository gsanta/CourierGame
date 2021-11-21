using UnityEngine;
using UnityEngine.AI;
using Zenject;
using System;
using Delivery;
using AI;
using Service;
using Attacks;
using UI;

namespace Bikers
{
    public class Biker : MonoBehaviour, IGameObject
    {
        [SerializeField]
        public Transform viewPoint;
        [SerializeField]
        private CharacterController charController;
        [SerializeField]
        public Transform packageHolder;

        public Package package;
        private string courierName;

        private CurrentRole currentRole = CurrentRole.NONE;
        private bool isPaused = false;

        private EventService eventService;
        private AgentFactory agentFactory;
        private GoapAgent<Biker> agent;
        private IGoalProvider goalProvider;

        public NavMeshAgent navMeshAgent;

        public GoapAgent<Biker> Agent { get => agent; set => agent = value; }
        public EventService EventService { set => eventService = value; }
        public IGoalProvider GoalProvider { get => goalProvider; set => goalProvider = value; }

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            
            
            Transform attackRadius = transform.Find("Attack Radius");
            if (attackRadius)
            {
                //attackRadius.GetComponent<AttackRadius>().SetCanvasStore()
            }
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
                SetCurrentRole(CurrentRole.NONE);
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

        public void SetCurrentRole(CurrentRole currentRole)
        {
            if (this.currentRole != currentRole)
            {
                this.currentRole = currentRole;

                if (currentRole == CurrentRole.PLAY)
                {
                    InitPlayRole();
                }
                else
                {
                    FinishPlayRole();
                }

                eventService.EmitBikerCurrentRoleChanged(this);
                CurrentRoleChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public CurrentRole GetCurrentRole()
        {
            return currentRole;
        }

        private void FinishPlayRole()
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            agent.Active = true;
        }

        private void InitPlayRole()
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;

            agent.Active = false;
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

        public event EventHandler CurrentRoleChanged;
        public event EventHandler Updated;

        public class Factory : PlaceholderFactory<UnityEngine.Object, Biker>
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
