using Domain;
using System;

namespace Service
{
    public class MinimapPackageUpdater
    {
        private readonly MinimapStore minimapStore;
        private readonly PackageStore packageStore;

        public MinimapPackageUpdater(MinimapStore minimapStore, PackageStore pacakgeStore)
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
                package.Target.gameObject.SetActive(false);
            });

            minimapStore.VisiblePackages.ForEach(gameObject => gameObject.SetActive(true));
            minimapStore.VisiblePackageTargets.ForEach(gameObject => gameObject.SetActive(true));
        }
    }
}
