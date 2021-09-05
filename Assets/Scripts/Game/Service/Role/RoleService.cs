using System;
using Zenject;

namespace Service
{
    public class RoleService
    {
        private readonly BikerService bikerService;
        private readonly PackageStore packageStore;

        [Inject]
        public RoleService(BikerService bikerService, PackageStore packageStore)
        {
            this.bikerService = bikerService;
            this.packageStore = packageStore;
        }

        public void SetCurrentRole(CurrentRole currentRole, Biker biker)
        {
            var prevBiker = bikerService.FindPlayOrFollowRole();

            if (prevBiker && prevBiker != biker && (currentRole == CurrentRole.PLAY || currentRole == CurrentRole.FOLLOW))
            {
                prevBiker.SetCurrentRole(CurrentRole.NONE);
            }

            biker.SetCurrentRole(currentRole);

            if (currentRole == CurrentRole.NONE)
            {
                packageStore.GetAll().ForEach(package => package.MinimapGameObject.SetActive(true));
            } else
            {
                packageStore.GetAll().ForEach(package => package.MinimapGameObject.SetActive(false));
                
                var activePackage = biker.GetPackage();
                if (activePackage != null)
                {
                    activePackage.MinimapGameObject.SetActive(true);
                }
            }
        }

        // TODO hide it from global space
        public void EmitCurrentRoleChanged()
        {
            CurrentRoleChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CurrentRoleChanged;
    }
}
