
using System;

namespace Route
{
    public class RouteSetup
    {
        private readonly RouteBuilder routeFacade;

        public RouteSetup(RoadStore roadStore, RouteBuilder routeFacade)
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
