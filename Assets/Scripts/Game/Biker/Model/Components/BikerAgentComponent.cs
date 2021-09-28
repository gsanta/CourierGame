using Service;
using AI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Delivery;
using Model;
using Route;

namespace Bikers
{
    public class BikerAgentComponent : MonoBehaviour, IGoapAgentInjections<Biker>, IGoapAgentProvider<Biker>
    {
        [SerializeField]
        private string agentId;
        private GoapAgent<Biker> goapAgent;
        private bool isActivated = false;
        private BikerActionProvider bikerActionProvider;

        public void Construct(BikerActionProvider bikerActionProvider)
        {
            this.bikerActionProvider = bikerActionProvider;
        }

        public GoapAgent<Biker> GoapAgent { get => goapAgent; }

        public NavMeshAgent GetNavMeshAgent()
        {
            return GetComponent<NavMeshAgent>();
        }

        public void SetActivated(bool isActivated)
        {
            if (this.isActivated != isActivated)
            {
                this.isActivated = isActivated;
                if (!isActivated)
                {
                    goapAgent.AbortAction();
                }
            }
        }

        private void Start()
        {
            Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
            goals.Add(new SubGoal("isPackageDropped", 1, true), 3);
            //goals.Add(new SubGoal("isTestFinished", 1, true), 3);

            List<GoapAction<Biker>> actions = bikerActionProvider.CloneActions(this);

            goapAgent = new GoapAgent<Biker>(agentId, this, actions, goals);
        }

        public void CompleteAction()
        {
            goapAgent.CompleteAction();
        }

        public WorldStates GetWorldStates()
        {
            var worldStates = new WorldStates();

            var package = GetCharachter().GetPackage();
            if (package)
            {
                if (package.Status == DeliveryStatus.ASSIGNED)
                {
                    worldStates.AddState("isPackageReserved", 3);
                    worldStates.AddState("isPackagePickedUp", 3);
                }
                else if (package.Status == DeliveryStatus.RESERVED)
                {
                    worldStates.AddState("isPackageReserved", 3);
                }
            }

            return worldStates;
        }

        public GoapAgent<Biker> GetGoapAgent()
        {
            return goapAgent;
        }

        private void LateUpdate()
        {
            if (isActivated)
            {
                goapAgent.Update();
            }
        }

        public Biker GetCharachter()
        {
            return GetComponentInParent<Biker>();
        }

        public class Factory : PlaceholderFactory<Object, BikerAgentComponent>
        {
        }
    }
}
