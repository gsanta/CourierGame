﻿using System;
using System.Collections.Generic;
using AI;

namespace Domain
{
    public class AssignPackageAction : GoapAction<Biker>
    {

        private PackageStore packageStore;

        public AssignPackageAction(IGoapAgentProvider<Biker> goapAgentProvider, PackageStore packageStore) : base(goapAgentProvider)
        {
            this.packageStore = packageStore;
        }

        public override bool IsDestinationReached()
        {
            return true;
        }

        public override bool PostAbort()
        {
            return true;
        }

        public override bool PostPerform()
        {
            List<Package> packages = packageStore.GetAllPickable();

            if (packages.Count == 0)
            {
                return false;
            } else
            {
                int selectedIndex = UnityEngine.Random.Range(0, packages.Count);
                Package selectedPackage = packages[0];

                selectedPackage.ReservePackage(GoapAgent.Parent);

                target = selectedPackage.gameObject;

                return true;
            }
        }

        public override bool PrePerform()
        {
            return true;
        }

        protected override WorldState[] GetAfterEffects()
        {
            return new WorldState[] { new WorldState("isPackageReserved", 3) };
        }

        protected override WorldState[] GetPreConditions()
        {
            return new WorldState[0];
        }
    }
}