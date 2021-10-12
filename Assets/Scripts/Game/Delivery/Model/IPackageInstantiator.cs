using UnityEngine;

namespace Delivery
{
    public interface IPackageInstantiator
    {
        Package InstantitatePackage();
        GameObject InstantiateMinimapPackage();
        GameObject InstantiatePackageTarget();
        GameObject InstantiatePackageTargetOnMinimap();
    }
}
