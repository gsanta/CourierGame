using UnityEngine;

namespace Delivery
{
    public interface IPackageStore
    {
        void SetPackageTemplate(Package packageTemplate);
        void SetPackageTargetTemplate(GameObject packageTargetTemplate);
        void SetPackageMinimapTemplate(GameObject packageMinimapTemplate);
        void SetPackageTargetMinimapTemplate(GameObject packageTargetMinimapTemplate);
    }
}
