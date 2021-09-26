using Bikers;
using Delivery;
using System;

namespace Model
{
    public interface IEventService
    {
        void EmitPackageStatusChanged(Package package);
        event EventHandler<PackageStatusChangedEventArgs> PackageStatusChanged;

        void EmitBikerCurrentRoleChanged(Biker biker);
        event EventHandler<BikerCurrentRoleChangedEventArgs> BikerCurrentRoleChanged;
    }
}
