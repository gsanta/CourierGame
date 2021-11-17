using Bikers;
using Cameras;
using Controls;
using Routes;
using RSG;

namespace GamePlay
{
    public class PlayerCommandTurns : ITurns
    {
        private TurnHelper turnHelper;
        private readonly BikerStore playerStore;
        private readonly RouteStore routeStore;
        private readonly RouteTool routeTool;
        private readonly CameraController cameraController;
        private Promise promise;

        public PlayerCommandTurns(TurnHelper turnHelper, BikerStore playerStore, RouteStore routeStore, RouteTool routeTool, CameraController cameraController)
        {
            this.turnHelper = turnHelper;
            this.playerStore = playerStore;
            this.routeStore = routeStore;
            this.routeTool = routeTool;
            this.cameraController = cameraController;
        }

        public Promise Execute()
        {
            promise = new Promise();
            playerStore.SetActivePlayer(playerStore.GetFirstPlayer());
            cameraController.PanTo(playerStore.GetActivePlayer());

            return promise;
        }

        public void Step()
        {
            if (playerStore.GetActivePlayer() == playerStore.GetLastPlayer())
            {
                routeStore.AddRoute(playerStore.GetActivePlayer(), routeTool.GetPoints());
                promise.Resolve();
            }
            else
            {
                routeStore.AddRoute(playerStore.GetActivePlayer(), routeTool.GetPoints());
                turnHelper.ChangePlayer(playerStore.GetNextPlayer());
                routeTool.Step();
            }
        }

        public void Reset()
        {

        }
    }
}
