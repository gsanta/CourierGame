using UnityEngine;
using UnityEngine.AI;
using Zenject;
using System;
using Model;
using Delivery;
using AI;

namespace Bikers
{
    public class Biker : MonoBehaviour
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

        private IEventService eventService;
        private AgentFactory agentFactory;

        public GoapAgent<Biker> agent;
        private BikerPlayComponent player;

        public NavMeshAgent navMeshAgent;

        public void Construct(IEventService eventService, AgentFactory agentFactory)
        {
            this.eventService = eventService;
            this.agentFactory = agentFactory;
        }

        protected void Start()
        {
            player = GetComponent<BikerPlayComponent>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            agent = agentFactory.CreateBikerAgent(this);
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
            player.SetActivated(false);
        }

        private void InitPlayRole()
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;

            player.SetActivated(true);
            agent.Active = false;
        }

        public void CompleteAction()
        {
            agent.CompleteAction();
        }

        public event EventHandler CurrentRoleChanged;

        public class Factory : PlaceholderFactory<UnityEngine.Object, Biker>
        {
        }
    }
}
