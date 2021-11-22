using Bikers;
using Cameras;
using Controls;
using Enemies;
using GizmoNS;
using Pedestrians;
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
        private PedestrianStore pedestrianStore;
        private EnemyStore enemyStore;
        private ArrowRendererProvider arrowRendererProvider;

        public PlayerCommandTurns(TurnHelper turnHelper, BikerStore playerStore, RouteStore routeStore, RouteTool routeTool, CameraController cameraController, PedestrianStore pedestrianStore, EnemyStore enemyStore, ArrowRendererProvider arrowRendererProvider)
        {
            this.turnHelper = turnHelper;
            this.playerStore = playerStore;
            this.routeStore = routeStore;
            this.routeTool = routeTool;
            this.cameraController = cameraController;
            this.enemyStore = enemyStore;
            this.pedestrianStore = pedestrianStore;
            this.arrowRendererProvider = arrowRendererProvider;

            routeTool.RouteFinished += HandleRouteFinished;
        }

        private void HandleRouteFinished(object sender, EventArgs args)
        {
            if (routeTool.IsValidRoute())
            {
                AddPlayerRoute();
                ChooseNextPlayer();
            }
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
                routeTool.Enabled = false;
                arrowRendererProvider.ArrowRenderer.Enabled = false;
                promise.Resolve();
            } else
            {
                var usedPlayers = new HashSet<Player>(routeStore.GetRoutes().Keys);
                var players = playerStore.GetAll();
                var index = players.IndexOf(playerStore.GetActivePlayer());
                var end = players.GetRange(index, players.Count - index);
                var start = players.GetRange(0, index);
                var list = new List<Player>();
                list.AddRange(start);
                list.AddRange(end);

                var nextPlayer = list.Find(item => usedPlayers.Contains(item) == false);
                turnHelper.ChangePlayer(nextPlayer, false);
            }
        }

        public Promise Execute()
        {
            arrowRendererProvider.ArrowRenderer.SetStart(playerStore.GetActivePlayer().transform.position);
            arrowRendererProvider.ArrowRenderer.Enabled = true;
            pedestrianStore.GetAll().ForEach(pedestrian => pedestrian.Agent.AbortAction());
            enemyStore.GetAll().ForEach(pedestrian => pedestrian.Agent.AbortAction());

            routeTool.Enabled = true;

            promise = new Promise();
            playerStore.SetActivePlayer(playerStore.GetFirstPlayer());
            cameraController.PanTo(playerStore.GetActivePlayer());

            return promise;
        }

        public void Step()
        {
            routeTool.ClearRoute();
            ChooseNextPlayer();
        }

        public void Reset()
        {

        }
    }
}
