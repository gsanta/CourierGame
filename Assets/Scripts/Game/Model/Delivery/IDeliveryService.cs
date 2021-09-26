using Bikers;
using System;

namespace Model
{
    public interface IDeliveryService
    {
        void ReservePackage(Package package, Biker biker);
        void AssignPackage(Package package);
        void DeliverPackage(Package package, bool checkRange);
    }
}
