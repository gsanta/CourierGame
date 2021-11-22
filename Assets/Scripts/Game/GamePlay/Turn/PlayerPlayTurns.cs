
using Actions;
using AI;
using Bikers;
using Cameras;
using Controls;
using Enemies;
using Pedestrians;
using Routes;
using RSG;
using System.Collections.Generic;

namespace GamePlay
{
    public class PlayerPlayTurns : ITurns
    {
        private readonly BikerStore playerStore;
        private readonly EnemyStore enemyStore;
        private readonly PedestrianStore pedestrianStore;
        private readonly RouteStore routeStore;
        private readonly RouteTool routeTool;
        private readonly ActionFactory actionFactory;
        private readonly CameraController cameraController;
        private Promise promise;

        private ISet<Player> movingPlayers = new HashSet<Player>();

        public PlayerPlayTurns(BikerStore playerStore, EnemyStore enemyStore, PedestrianStore pedestrianStore, RouteStore routeStore, RouteTool routeTool, ActionFactory actionFactory, CameraController cameraController)
        {
            this.playerStore = playerStore;
            this.enemyStore = enemyStore;
            this.pedestrianStore = pedestrianStore;
            this.routeStore = routeStore;
            this.routeTool = routeTool;
            this.actionFactory = actionFactory;
            this.cameraController = cameraController;
        }


        public Promise Execute()
        {
            promise = new Promise();

            playerStore.GetAll().ForEach(player => {
                movingPlayers.Add(player);
                player.Agent.Active = true;

                player.Agent.GoalReached += HandleGoalReached;
                var points = routeStore.GetRoutes()[player];
                points.RemoveAt(0);
                player.Agent.SetActions(actionFactory.CreatePlayerWalkAction(player.Agent, points));
                player.Agent.SetGoals(new Goal(AIStateName.WALK_FINISHED, false), false);
            });

            playerStore.SetActivePlayer(playerStore.GetFirstPlayer());
            cameraController.Follow(playerStore.GetActivePlayer());

            enemyStore.GetAll().ForEach(enemy => {
                enemy.Agent.Active = true;
                enemy.Agent.NavMeshAgent.isStopped = false;

                enemy.Agent.SetActions(actionFactory.CreateEnemyWalkAction(enemy.Agent));
                enemy.Agent.SetGoals(enemy.GetGoalProvider().CreateGoal(), false);
            });

            pedestrianStore.GetAll().ForEach(pedestrian => {
                pedestrian.Agent.Active = true;
                pedestrian.Agent.NavMeshAgent.isStopped = false;

                pedestrian.Agent.SetActions(actionFactory.CreatePedestrianWalkAction(pedestrian.Agent));
                pedestrian.Agent.SetGoals(pedestrian.GetGoalProvider().CreateGoal(), false);
            });

            return promise;
        }

        private void Finish()
        {
            pedestrianStore.GetAll().ForEach(pedestrian => pedestrian.Agent.AbortAction());
            enemyStore.GetAll().ForEach(pedestrian => pedestrian.Agent.AbortAction());

            promise.Resolve();
        }

        public void Step()
        {

        }

        public void Reset()
        {

        }

        private void HandleGoalReached(object sender, GoalReachedEventArgs<Player> args)
        {
            movingPlayers.Remove(args.agent.Parent);
            args.agent.GoalReached -= HandleGoalReached;
            args.agent.Active = false;

            if (movingPlayers.Count == 0)
            {
                routeStore.Clear();
                routeTool.Reset();
                cameraController.Follow(null);
                Finish();
            }
        }
    }
}
