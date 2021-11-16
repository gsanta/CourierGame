using Bikers;
using Cameras;
using System.Collections.Generic;
using Zenject;

namespace GamePlay
{
    public class TurnManager
    {
        private readonly ITurns playerCommandTurns;
        private readonly ITurns playerPlayTurns;
        private readonly ITurns pedestrianTurns;
        private readonly ITurns enemyTurns;
        private ITurns activeTurn;
        private List<ITurns> turns;
        private BikerStore playerStore;
        private CameraController cameraController;

        public TurnManager(BikerStore playerStore, CameraController cameraController, [Inject(Id = "PlayerCommandTurns")] ITurns playerCommandTurns, [Inject(Id = "PlayerPlayTurns")] ITurns playerPlayTurns, [Inject(Id = "PedestrianTurns")] ITurns pedestrianTurns, [Inject(Id = "EnemyTurns")] ITurns enemyTurns)
        {
            this.playerStore = playerStore;
            this.cameraController = cameraController;
            this.playerCommandTurns = playerCommandTurns;
            this.playerPlayTurns = playerPlayTurns;
            this.pedestrianTurns = pedestrianTurns;
            this.enemyTurns = enemyTurns;
            turns = new List<ITurns>
            {
                playerCommandTurns,
                playerPlayTurns,
                pedestrianTurns,
                enemyTurns
            };
        }

        public bool IsPlayerCommandTurn()
        {
            return activeTurn == playerCommandTurns;
        }

        public void Step()
        {
            if (activeTurn == null)
            {
                turns.ForEach(turn => turn.Reset());
                activeTurn = playerCommandTurns;
                playerCommandTurns.Execute()
                    .Then(() => {
                        activeTurn = playerPlayTurns;
                        return playerPlayTurns.Execute();
                    })
                    .Then(() => {
                        activeTurn = pedestrianTurns;
                        return pedestrianTurns.Execute();
                    })
                    .Then(() =>
                    {
                        activeTurn = enemyTurns;
                        return enemyTurns.Execute();
                    })
                    .Then(() => {
                        activeTurn = null;
                        Step();
                    });
            } else
            {
                activeTurn.Step();
            }


            //if (!isPlayMode && bikerStore.GetActivePlayer() == bikerStore.GetLastPlayer())
            //{
            //    isPlayMode = true;
            //    pedestrianStore.GetAll().ForEach(pedestrian => pedestrian.Agent.Active = true);
            //    bikerStore.GetAll().ForEach(player => player.Agent.GoalReached += HandleGoalReached);
            //    routeStore.AddRoute(bikerStore.GetActivePlayer(), routeTool.GetPoints());
            //    SetNextPlayer();
            //}

            //if (!isPlayMode)
            //{
            //    routeStore.AddRoute(bikerStore.GetActivePlayer(), routeTool.GetPoints());
            //    SetNextPlayer();
            //    routeTool.Step();
            //}
            //else
            //{
            //    var player = bikerStore.GetActivePlayer();
            //    player.Agent.Active = true;
            //    var points = routeStore.GetRoutes()[player];
            //    points.RemoveAt(0);
            //    player.Agent.SetActions(actionFactory.CreatePlayerWalkAction(player.Agent, points));
            //    player.Agent.SetGoals(new Goal(AIStateName.WALK_FINISHED, false), false);
            //}
        }

        public void ChangePlayer(Biker player)
        {
            playerStore.SetActivePlayer(player);
            cameraController.Follow(playerStore.GetActivePlayer());
        }

        //private void HandleGoalReached(object sender, GoalReachedEventArgs<Biker> args)
        //{
        //    args.agent.GoalReached -= HandleGoalReached;
        //    args.agent.Active = false;

        //    if (args.agent.Parent == bikerStore.GetLastPlayer())
        //    {
        //        isPlayMode = false;
        //        routeStore.Clear();
        //        routeTool.Reset();
        //        SetNextPlayer();
        //    }
        //    else
        //    {
        //        SetNextPlayer();
        //        Play();
        //    }
        //}

        //private void SetNextPlayer()
        //{
        //    bikerStore.SetNextPlayer();
        //    cameraController.PanTo(bikerStore.GetActivePlayer().transform.position);
        //}
    }
}
