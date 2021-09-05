
using System;

namespace Service
{
    public class EventService
    {

        public void EmitPackageStatusChanged(Package package)
        {
            PackageStatusChanged?.Invoke(this, new PackageStatusChangedEventArgs(package));
        }

        public event EventHandler<PackageStatusChangedEventArgs> PackageStatusChanged;
    }
}
