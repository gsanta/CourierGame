using Bikers;
using Service;
using Stats;
using UnityEngine;

namespace Delivery
{
    public class DeliveryService : IDeliveryService
    {
        private readonly EventService eventService;
        private readonly PackageStore packageStore;
        private readonly MoneyStore moneyStore;

        public DeliveryService(EventService eventService, PackageStore packageStore, MoneyStore moneyStore)
        {
            this.eventService = eventService;
            this.packageStore = packageStore;
            this.moneyStore = moneyStore;
        }

        public void ReservePackage(Package package, Biker biker)
        {
            if (package.Status == DeliveryStatus.UNASSIGNED)
            {
                package.Status = DeliveryStatus.RESERVED;
                biker.SetPackage(package);
                package.Biker = biker;

                eventService.EmitPackageStatusChanged(package);
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
            package.Target.gameObject.SetActive(true);
            eventService.EmitPackageStatusChanged(package);
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
                package.Target.gameObject.SetActive(true);
                packageStore.Remove(package);
                biker.SetPackage(null);
                biker = null;
                moneyStore.AddMoney(package.Price);

                package.Status = DeliveryStatus.DELIVERED;

                eventService.EmitPackageStatusChanged(package);
                package.DestroyPackage();
            }
        }
    }
}
