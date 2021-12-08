using AI;
using GameObjects;
using Enemies;
using GamePlay;
using Pedestrians;
using System.Collections.Generic;
using UnityEngine;

namespace Actions
{
    public class ActionFactory
    {
        public ActionCreators actionCreators = new ActionCreators();
        private SceneManager sceneManager;

        public ActionFactory(SceneManager sceneManager)
        {
            this.sceneManager = sceneManager;
        }

        public PlayerWalkAction CreatePlayerWalkAction(GoapAgent<GameCharacter> agent, List<Vector3> points)
        {
            return actionCreators.PlayerWalkActionCreator.Create(agent, points);
        }

        public PlayerWalkIntoBuildingAction CreatePlayerWalkIntoBuildingAction(GoapAgent<GameCharacter> agent, List<Vector3> points)
        {
            return actionCreators.PlayerWalkIntoBuildingActionCreator.Create(agent, points);
        }

        public WalkAction<Pedestrian> CreatePedestrianWalkAction(GoapAgent<Pedestrian> agent)
        {
            return actionCreators.PedestrianWalkActionCreator.Create(agent, new AIStateName[] { }, new AIStateName[] { AIStateName.DESTINATION_REACHED });
        }

        public WalkAction<GameCharacter> CreateEnemyWalkAction(GoapAgent<GameCharacter> agent)
        {
            return actionCreators.EnemyWalkActionCreator.Create(agent, new AIStateName[] { }, new AIStateName[] { AIStateName.DESTINATION_REACHED });
        }

        public EnterBuildingPostAction CreateEnterBuildingPostAction()
        {
            return (EnterBuildingPostAction) actionCreators.EnterBuildingPostAction.Clone();
        }

        public ExitBuildingPostAction CreateExitBuildingPostAction()
        {
            return (ExitBuildingPostAction) actionCreators.ExitBuildingPostAction.Clone();
        }
    }
}
