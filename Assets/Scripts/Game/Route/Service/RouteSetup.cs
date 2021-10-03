
using System;

namespace Route
{
    public class RouteSetup
    {
        private readonly RouteFacade routeFacade;

        public RouteSetup(RoadStore roadStore, RouteFacade routeFacade)
        {

            roadStore.Initialized += HandleRoadInitialized;
            this.routeFacade = routeFacade;
        }

        private void HandleRoadInitialized(object sender, EventArgs args)
        {
            routeFacade.Setup();
        }
    }
}
