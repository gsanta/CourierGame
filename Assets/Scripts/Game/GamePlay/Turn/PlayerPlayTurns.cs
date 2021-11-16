﻿
using Actions;
using AI;
using Bikers;
using Cameras;
using Controls;
using Pedestrians;
using Routes;
using RSG;

namespace GamePlay
{
    public class PlayerPlayTurns : ITurns
    {
        private readonly BikerStore playerStore;
        private readonly PedestrianStore pedestrianStore;
        private readonly RouteStore routeStore;
        private readonly RouteTool routeTool;
        private readonly ActionFactory actionFactory;
        private readonly CameraController cameraController;
        private TurnManager turnManager;
        private Promise promise;

        public PlayerPlayTurns(TurnManager turnManager, BikerStore playerStore, PedestrianStore pedestrianStore, RouteStore routeStore, RouteTool routeTool, ActionFactory actionFactory, CameraController cameraController)
        {
            this.turnManager = turnManager;
            this.playerStore = playerStore;
            this.pedestrianStore = pedestrianStore;
            this.routeStore = routeStore;
            this.routeTool = routeTool;
            this.actionFactory = actionFactory;
            this.cameraController = cameraController;
        }


        public Promise Execute()
        {
            promise = new Promise();

            playerStore.SetActivePlayer(playerStore.GetFirstPlayer());
            cameraController.Follow(playerStore.GetActivePlayer());
            NextStep();

            return promise;
        }

        public void Step()
        {

        }

        public void Reset()
        {

        }

        private void NextStep()
        {
            var player = playerStore.GetActivePlayer();
            player.Agent.GoalReached += HandleGoalReached;
            player.Agent.Active = true;
            var points = routeStore.GetRoutes()[player];
            points.RemoveAt(0);
            player.Agent.SetActions(actionFactory.CreatePlayerWalkAction(player.Agent, points));
            player.Agent.SetGoals(new Goal(AIStateName.WALK_FINISHED, false), false);
        }

        private void HandleGoalReached(object sender, GoalReachedEventArgs<Biker> args)
        {
            args.agent.GoalReached -= HandleGoalReached;
            args.agent.Active = false;

            if (args.agent.Parent == playerStore.GetLastPlayer())
            {
                //isPlayMode = false;
                routeStore.Clear();
                routeTool.Reset();
                promise.Resolve();
                cameraController.Follow(null);
            }
            else
            {
                SetNextPlayer();
                NextStep();
            }
        }

        private void SetNextPlayer()
        {
            playerStore.SetNextPlayer();
            cameraController.Follow(playerStore.GetActivePlayer());
        }
    }
}