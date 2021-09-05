using Domain;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Service
{
    public class MinimapPackageSetter
    {
        private readonly IEventService eventService;
        private readonly MinimapStore minimapStore;
        private readonly PackageStore packageStore;
        private readonly BikerService bikerService;

        [Inject]
        public MinimapPackageSetter(IEventService eventService, MinimapStore minimapStore, PackageStore packageStore, BikerService bikerService)
        {
            this.eventService = eventService;
            this.minimapStore = minimapStore;
            this.packageStore = packageStore;
            this.bikerService = bikerService;
            this.eventService.BikerCurrentRoleChanged += HandleBikerRoleChanged;
            this.eventService.PackageStatusChanged += HandlePackageStatusChanged;
        }

        private void HandleBikerRoleChanged(object sender, BikerCurrentRoleChangedEventArgs args)
        {
            UpdateMinimapStore();
        }

        private void HandlePackageStatusChanged(object sender, PackageStatusChangedEventArgs args)
        {
            UpdateMinimapStore();
        }

        private void UpdateMinimapStore()
        {
            var biker = bikerService.FindPlayOrFollowRole();
        
            if (biker && biker.package)
            {
                minimapStore.VisiblePackages = new List<GameObject> { biker.package.MinimapGameObject };
                if (biker.package.Status == DeliveryStatus.ASSIGNED || biker.package.Status == DeliveryStatus.RESERVED)
                {
                    minimapStore.VisiblePackageTargets = new List<GameObject> { biker.package.TargetMinimapGameObject };
                } else
                {
                    minimapStore.VisiblePackageTargets = new List<GameObject>();
                }
            } else
            {
                minimapStore.VisiblePackages = packageStore.GetAll().Select(package => package.MinimapGameObject).ToList();
                minimapStore.VisiblePackageTargets = new List<GameObject>();
            }
        }
    }
}
