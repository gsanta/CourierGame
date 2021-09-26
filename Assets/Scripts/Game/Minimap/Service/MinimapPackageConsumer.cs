using Delivery;
using System;

namespace Minimap
{
    public class MinimapPackageConsumer
    {
        private readonly MinimapStore minimapStore;
        private readonly PackageStore packageStore;

        public MinimapPackageConsumer(MinimapStore minimapStore, PackageStore pacakgeStore)
        {
            this.minimapStore = minimapStore;
            this.packageStore = pacakgeStore;

            minimapStore.StoreChanged += HandleStoreChanged;
        }

        public void HandleStoreChanged(object sender, EventArgs args)
        {
            packageStore.GetAll().ForEach(package =>
            {
                package.MinimapGameObject.SetActive(false);
                package.TargetMinimapGameObject.SetActive(false);
            });

            minimapStore.VisiblePackages.ForEach(gameObject => gameObject.SetActive(true));
            minimapStore.VisiblePackageTargets.ForEach(gameObject => gameObject.SetActive(true));
        }
    }
}
