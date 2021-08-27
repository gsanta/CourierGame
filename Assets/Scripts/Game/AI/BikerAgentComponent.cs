using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace AI
{
    public class BikerAgentComponent : MonoBehaviour, IGoapAgentInjections<Biker>, IGoapAgentProvider<Biker>
    {
        [SerializeField]
        private string agentId;
        private GoapAgent<Biker> goapAgent;
        private PackageStore packageStore;

        [Inject]
        public void Construct(PackageStore packageStore)
        {
            this.packageStore = packageStore;
        }


        public GoapAgent<Biker> GoapAgent { get => goapAgent; }

        public NavMeshAgent GetNavMeshAgent()
        {
            return GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
            goals.Add(new SubGoal("isPackageDropped", 1, true), 3);
            
            List<GAction<Biker>> actions = new List<GAction<Biker>>();

            actions.Add(new AssignPackageAction(this, packageStore));
            actions.Add(new DeliverPackageAction(this));
            actions.Add(new PickUpPackageAction(this));

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
            goapAgent.Update();
        }

        public Biker GetCharachter()
        {
            return GetComponentInParent<Biker>();
        }

        public class Factory : PlaceholderFactory<UnityEngine.Object, BikerAgentComponent>
        {
        }

    }
}
