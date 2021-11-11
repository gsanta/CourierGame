using Bikers;
using Scenes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Delivery
{
    public class DeliveryStore : IResetable
    {
        private Dictionary<Biker, Package> packageMap = new Dictionary<Biker, Package>();
        private Dictionary<Package, Biker> reversePackageMap = new Dictionary<Package, Biker>();
        private PackageStore packageStore;

        public DeliveryStore(PackageStore packageStore)
        {
            this.packageStore = packageStore;
        }

        public void AssignPackageToPlayer(Biker courier, Package package)
        {
            package.Status = DeliveryStatus.ASSIGNED;
            packageMap.Add(courier, package);
            reversePackageMap.Add(package, courier);

            OnDeliveryStatusChanged?.Invoke(this, EventArgs.Empty);
        }

        public void DropPackage(Package package)
        {
            Biker courier;
            if (reversePackageMap.TryGetValue(package, out courier))
            {
                packageMap.Remove(courier);
            }
            reversePackageMap.Remove(package);

            Vector2 packagePos = new Vector2(package.transform.position.x, package.transform.position.z);
            Vector2 targetPos = new Vector2(package.Target.transform.position.x, package.Target.transform.position.z);
            if (Vector2.Distance(packagePos, targetPos) < 1)
            {
                packageStore.Remove(package);
                package.DestroyPackage();
                package.Status = DeliveryStatus.DELIVERED;
            }
            else
            {
                package.Status = DeliveryStatus.UNASSIGNED;
            }

            OnDeliveryStatusChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool GetPackage(Biker courier, out Package package)
        {
            return packageMap.TryGetValue(courier, out package);
        }

        public List<Package> AssignedPackages
        {
            get => packageStore.GetAll().FindAll(package => GetPlayerForPackage(package) != null);
        }

        public List<Package> UnAssignedPackages
        {
            get => packageStore.GetAll().FindAll(package => GetPlayerForPackage(package) == null);
        }

        public Biker GetPlayerForPackage(Package package)
        {
            Biker courier;

            reversePackageMap.TryGetValue(package, out courier);

            return courier;
        }

        public void Reset()
        {
            packageMap = new Dictionary<Biker, Package>();
            reversePackageMap = new Dictionary<Package, Biker>();
            packageStore = null;
        }

        public event EventHandler OnDeliveryStatusChanged;
    }
}

