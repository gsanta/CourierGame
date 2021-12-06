﻿
using Actions;
using AI;
using GameObjects;
using Cameras;
using Controls;
using Enemies;
using GameObjects;
using Pedestrians;
using Routes;
using RSG;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class PlayerPlayTurns : ITurns
    {
        private readonly PlayerStore playerStore;
        private readonly EnemyStore enemyStore;
        private readonly PedestrianStore pedestrianStore;
        private readonly RouteStore routeStore;
        private readonly RouteTool routeTool;
        private readonly ActionFactory actionFactory;
        private readonly CameraController cameraController;
        private readonly SceneManagerHolder sceneManager;
        private readonly SubsceneCharacterStore subsceneCharacterStore;
        private Promise promise;

        private ISet<GameCharacter> movingPlayers = new HashSet<GameCharacter>();

        public PlayerPlayTurns(PlayerStore playerStore, EnemyStore enemyStore, PedestrianStore pedestrianStore, RouteStore routeStore, RouteTool routeTool, ActionFactory actionFactory, CameraController cameraController, SceneManagerHolder sceneManager, SubsceneCharacterStore subsceneCharacterStore)
        {
            this.playerStore = playerStore;
            this.enemyStore = enemyStore;
            this.pedestrianStore = pedestrianStore;
            this.routeStore = routeStore;
            this.routeTool = routeTool;
            this.actionFactory = actionFactory;
            this.cameraController = cameraController;
            this.sceneManager = sceneManager;
            this.subsceneCharacterStore = subsceneCharacterStore;
        }

        public Promise Execute()
        {
            promise = new Promise();

            playerStore.GetAll().ForEach(player => {
                movingPlayers.Add(player);
                player.Agent.Active = true;
                player.Agent.NavMeshAgent.enabled = true;
                player.Agent.NavMeshAgent.isStopped = false;

                player.Agent.GoalReached += HandleGoalReached;
                player.Agent.SetActions(GetPlayerAction(player));
                player.Agent.SetGoals(new Goal(AIStateName.WALK_FINISHED, false), false);
            });

            playerStore.SetActivePlayer(playerStore.GetFirstPlayer());
            //cameraController.Follow(playerStore.GetActivePlayer());

            enemyStore.GetAll().ForEach(enemy => {
                enemy.Agent.Active = true;
                enemy.Agent.NavMeshAgent.enabled = true;
                enemy.Agent.NavMeshAgent.isStopped = false;

                enemy.Agent.SetActions(actionFactory.CreateEnemyWalkAction(enemy.Agent));
                enemy.Agent.SetGoals(enemy.GetGoalProvider().CreateGoal(), false);
            });

            pedestrianStore.GetAll().ForEach(pedestrian => {
                pedestrian.Agent.Active = true;
                pedestrian.Agent.NavMeshAgent.enabled = true;
                pedestrian.Agent.NavMeshAgent.isStopped = false;

                pedestrian.Agent.SetActions(actionFactory.CreatePedestrianWalkAction(pedestrian.Agent));
                pedestrian.Agent.SetGoals(pedestrian.GetGoalProvider().CreateGoal(), false);
            });

            return promise;
        }

        private GoapAction<GameCharacter> GetPlayerAction(GameCharacter player)
        {
            var points = routeStore.GetRoutes()[player];
            points.RemoveAt(0);

            if (TagManager.IsBuilding(routeTool.GetTag())) {
                var action = actionFactory.CreatePlayerWalkIntoBuildingAction(player.Agent, points);
                action.AddPostAction(actionFactory.CreateEnterBuildingPostAction());
                return action;
            } else if (TagManager.IsExitBuilding(routeTool.GetTag()))
            {
                var action = actionFactory.CreatePlayerWalkIntoBuildingAction(player.Agent, points);
                action.AddPostAction(actionFactory.CreateExitBuildingPostAction());
                return action;
            }
            else
            {
                return actionFactory.CreatePlayerWalkAction(player.Agent, points);
            }
        }

        private void Finish()
        {
            pedestrianStore.GetAll().ForEach(pedestrian => pedestrian.Agent.AbortAction());
            enemyStore.GetAll().ForEach(enemy => enemy.Agent.AbortAction());
            playerStore.GetAll().ForEach(player => player.Agent.AbortAction());

            var tuple = FindClosestEnemy();
            
            if (tuple.Item2 < 2)
            {
                subsceneCharacterStore.Players = new List<GameCharacter> { playerStore.GetActivePlayer() };
                subsceneCharacterStore.Enemies = new List<GameCharacter> { tuple.Item1 };
                sceneManager.D.EnterSubScene("Building");
            }

            promise.Resolve();
        }

        private (GameCharacter, float) FindClosestEnemy()
        {
            GameCharacter closestEnemy = null;
            float distance = float.MaxValue;
            GameCharacter player = playerStore.GetActivePlayer();

            foreach (var enemy in enemyStore.GetAll())
            {
                float dist = Vector3.Distance(enemy.transform.position, player.transform.position);
                if (dist < distance)
                {
                    distance = dist;
                    closestEnemy = enemy;
                }
            }

            return (closestEnemy, distance);
        }

        public void Pause()
        {
            pedestrianStore.GetAll().ForEach(pedestrian => pedestrian.Agent.NavMeshAgent.isStopped = true);
            enemyStore.GetAll().ForEach(enemy => enemy.Agent.NavMeshAgent.isStopped = true);
            playerStore.GetAll().ForEach(player => player.Agent.NavMeshAgent.isStopped = true);
        }

        public void Resume()
        {
            pedestrianStore.GetAll().ForEach(pedestrian => pedestrian.Agent.NavMeshAgent.isStopped = false);
            enemyStore.GetAll().ForEach(enemy => enemy.Agent.NavMeshAgent.isStopped = false);
            playerStore.GetAll().ForEach(player => player.Agent.NavMeshAgent.isStopped = false);
        }

        public void Step()
        {

        }

        public void Reset()
        {

        }

        public void Abort()
        {
            playerStore.GetAll().ForEach(player => player.Agent.GoalReached -= HandleGoalReached);
        }

        private void HandleGoalReached(object sender, GoalReachedEventArgs<GameCharacter> args)
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
