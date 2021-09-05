using Domain;
using UnityEngine;

namespace Service
{
    public class DeliveryService : IDeliveryService
    {
        private readonly EventService eventService;
        private readonly PackageStore packageStore;

        public DeliveryService(EventService eventService, PackageStore packageStore)
        {
            this.eventService = eventService;
            this.packageStore = packageStore;
        }

        public void ReservePackage(Package package, Biker biker)
        {
            if (package.Status == DeliveryStatus.UNASSIGNED)
            {
                package.Status = DeliveryStatus.RESERVED;
                biker.SetPackage(package);
                package.Biker = biker;

                eventService.EmitPackageStatusChanged(package);
                //HandleStatusChanged();
            }
        }

        public void AssignPackage(Package package)
        {
            if (package.Status != DeliveryStatus.RESERVED)
            {
                return;
            }

            var biker = package.Biker;

            package.gameObject.transform.position = biker.packageHolder.position;
            package.gameObject.transform.rotation = biker.packageHolder.rotation;
            package.gameObject.transform.parent = biker.packageHolder;
            package.Status = DeliveryStatus.ASSIGNED;
            package.Target.SetMeshVisibility(true);
            //HandleStatusChanged();
        }

        public void DeliverPackage(Package package, bool checkRange)
        {
            if (package.Status != DeliveryStatus.ASSIGNED)
            {
                return;
            }

            var packagePosition = package.gameObject.transform.position;
            var targetPosition = package.Target.transform.position;
            var biker = package.Biker;

            Vector2 packagePos = new Vector2(packagePosition.x, packagePosition.z);
            Vector2 targetPos = new Vector2(targetPosition.x, targetPosition.z);

            if (!checkRange || Vector2.Distance(packagePos, targetPos) < 2)
            {
                package.Target.SetMeshVisibility(false);
                packageStore.Remove(package);
                biker.SetPackage(null);
                biker = null;

                package.Status = DeliveryStatus.DELIVERED;

                package.DestroyPackage();
            }
        }
    }
}
