using Service;
using AI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Delivery;

namespace Model
{
    public class BikerAgentComponent : MonoBehaviour, IGoapAgentInjections<Biker>, IGoapAgentProvider<Biker>
    {
        [SerializeField]
        private string agentId;
        private GoapAgent<Biker> goapAgent;
        private PackageStore packageStore;
        private IDeliveryService deliveryService;
        private bool isActivated = false;
        private PackageStore2 packageStore2;
        private RouteAction routeAction;

        public void Construct(PackageStore packageStore, IDeliveryService deliveryService, PackageStore2 packageStore2, RouteAction routeAction)
        {
            this.packageStore = packageStore;
            this.deliveryService = deliveryService;
            this.packageStore2 = packageStore2;
            this.routeAction = routeAction;
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
            //goals.Add(new SubGoal("isPackageDropped", 1, true), 3);
            goals.Add(new SubGoal("isTestFinished", 1, true), 3);

            List<GoapAction<Biker>> actions = new List<GoapAction<Biker>>();

            actions.Add(new ReservePackageAction(this, packageStore, deliveryService));
            actions.Add(new DeliverPackageAction(this, deliveryService));
            actions.Add(new PickUpPackageAction(this, deliveryService));
            actions.Add(routeAction);
            routeAction.SetAgent(this);

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
