
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Bikers;

namespace Delivery
{
    public class PackageStore
    {
        private List<Package> packages = new List<Package>();

        private Package packageTemplate;
        private GameObject packageTargetTemplate;
        private GameObject packageMinimapTemplate;
        private GameObject packageTargetMinimapTemplate;

        public void SetPackageTemplate(Package packageTemplate)
        {
            this.packageTemplate = packageTemplate;
        }

        public Package GetPackageTemplate()
        {
            return packageTemplate;
        }

        public void SetPackageTargetTemplate(GameObject packageTargetTemplate)
        {
            this.packageTargetTemplate = packageTargetTemplate;
        }

        public GameObject GetPackageTargetTemplate()
        {
            return packageTargetTemplate;
        }

        public void SetPackageMinimapTemplate(GameObject packageMinimapTemplate)
        {
            this.packageMinimapTemplate = packageMinimapTemplate;
        }

        public GameObject GetPackageMinimapTemplate()
        {
            return packageMinimapTemplate;
        }

        public void SetPackageTargetMinimapTemplate(GameObject packageTargetMinimapTemplate)
        {
            this.packageTargetMinimapTemplate = packageTargetMinimapTemplate;
        }

        public GameObject GetPackageTargetMinimapTemplate()
        {
            return packageTargetMinimapTemplate;
        }

        public void Add(Package package)
        {
            packages.Add(package);
            TriggerPackageAdded(new PackageAddedEventArgs(package));
        }

        public void Remove(Package package)
        {
            packages.Remove(package);
        }

        public List<Package> GetAll()
        {
            return packages;
        }

        public List<Package> GetAllPickable()
        {
            return GetPackagesOfStatus(DeliveryStatus.UNASSIGNED);
        }

        public List<Package> GetPackagesOfStatus(DeliveryStatus status, params DeliveryStatus[] rest)
        {
            DeliveryStatus[] statuses = new DeliveryStatus[rest.Length + 1];
            statuses[0] = status;
            rest.CopyTo(statuses, 1);

            return packages.FindAll(package => statuses.Contains(package.Status));
        }

        public bool GetPackageWithinPickupRange(Biker biker, out Package deliveryPackage)
        {
            foreach (var package in packages)
            {
                if (Vector3.Distance(biker.GetTransform().position, package.transform.position) < 2)
                {
                    deliveryPackage = package;
                    return true;
                }
            }

            deliveryPackage = null;
            return false;
        }

        public event EventHandler<PackageAddedEventArgs> OnPackageAdded;

        private void TriggerPackageAdded(PackageAddedEventArgs e)
        {
            EventHandler<PackageAddedEventArgs> handler = OnPackageAdded;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

    public class PackageAddedEventArgs : EventArgs
    {
        private Package package;

        internal PackageAddedEventArgs(Package package)
        {
            this.package = package;
        }
        public Package Package { get => package; }
    }
}




