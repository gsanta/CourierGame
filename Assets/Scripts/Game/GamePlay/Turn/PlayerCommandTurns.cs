using Bikers;
using Cameras;
using Controls;
using Routes;
using RSG;
using System;
using System.Collections.Generic;

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

            routeTool.RouteFinished += HandleRouteFinished;
        }

        private void HandleRouteFinished(object sender, EventArgs args)
        {
            AddPlayerRoute();
            ChooseNextPlayer();
        }

        private void AddPlayerRoute()
        {
            if (routeTool.GetPoints().Count > 0)
            {
                routeStore.AddRoute(playerStore.GetActivePlayer(), routeTool.GetPoints());
                routeTool.SaveRoute();
            } else
            {
                routeTool.ClearRoute();
            }
        }

        private void ChooseNextPlayer()
        {
            var isLastPlayer = routeStore.GetRoutes().Count == playerStore.GetAll().Count;

            if (isLastPlayer)
            {
                promise.Resolve();
            } else
            {
                var usedPlayers = new HashSet<Biker>(routeStore.GetRoutes().Keys);
                var players = playerStore.GetAll();
                var index = players.IndexOf(playerStore.GetActivePlayer());
                var end = players.GetRange(index, players.Count - index);
                var start = players.GetRange(0, index);
                var list = new List<Biker>();
                list.AddRange(start);
                list.AddRange(end);

                var nextPlayer = list.Find(item => usedPlayers.Contains(item) == false);
                turnHelper.ChangePlayer(nextPlayer);
            }
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
            routeTool.ClearRoute();
            ChooseNextPlayer();
            //routeStore.AddRoute(playerStore.GetActivePlayer(), routeTool.GetPoints());
            //var isLastPlayer = routeStore.GetRoutes().Count == playerStore.GetAll().Count;

            //if (!isLastPlayer)
            //{
            //    var usedPlayers = new HashSet<Biker>(routeStore.GetRoutes().Keys);
            //    var players = playerStore.GetAll();
            //    var index = players.IndexOf(playerStore.GetActivePlayer());
            //    var end = players.GetRange(index, players.Count - index);
            //    var start = players.GetRange(0, index);
            //    var list = new List<Biker>();
            //    list.AddRange(start);
            //    list.AddRange(end);

            //    var selectedPlayer = list.Find(item => usedPlayers.Contains(item) == false);

            //    turnHelper.ChangePlayer(selectedPlayer);
            //    routeTool.SaveRoute();
            //} else
            //{
            //    promise.Resolve();
            //}
        }

        public void Reset()
        {

        }
    }
}
