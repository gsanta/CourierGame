using Delivery;
using Service;
using Zenject;

namespace Bikers
{
    public class RoleService
    {
        private readonly BikerService bikerService;
        private readonly PackageStore packageStore;
        private readonly EventService eventService;

        [Inject]
        public RoleService(BikerService bikerService, PackageStore packageStore, EventService eventService)
        {
            this.bikerService = bikerService;
            this.packageStore = packageStore;
            this.eventService = eventService;
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

            eventService.EmitBikerCurrentRoleChanged(biker);
        }
    }
}
