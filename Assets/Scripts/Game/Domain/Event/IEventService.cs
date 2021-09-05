using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IEventService
    {
        void EmitPackageStatusChanged(Package package);
        event EventHandler<PackageStatusChangedEventArgs> PackageStatusChanged;

        void EmitBikerCurrentRoleChanged(Biker biker);
        event EventHandler<BikerCurrentRoleChangedEventArgs> BikerCurrentRoleChanged;
    }
}
