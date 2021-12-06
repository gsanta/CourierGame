
using AI;
using GameObjects;
using Scenes;
using Enemies;
using Pedestrians;
using System;
using System.Collections.Generic;

namespace Agents
{
    public class ActionStore : IResetable
    {
        private List<GoapAction<Pedestrian>> pedestrianActions = new List<GoapAction<Pedestrian>>();
        private WalkAction<GameCharacter> enemyWalkAction;
        private WalkAction<Pedestrian> pedestrianWalkAction;
        private GoHomeAction goHomeAction;
        private Dictionary<Type, IActionCreator<IGameObject>> actionCreators = new Dictionary<Type, IActionCreator<IGameObject>>();
        private IActionCreator<Pedestrian> pedestrianActionCreator;
        private IActionCreator<GameCharacter> enemyActionCreator;

        public void SetPedestrianActionCreator(IActionCreator<Pedestrian> pedestrianActionCreator)
        {
            this.pedestrianActionCreator = pedestrianActionCreator;
        }

        public List<GoapAction<Pedestrian>> GetPedestrianActions()
        {
            return pedestrianActionCreator.GetActions();
        }

        public void SetEnemyActionCreator(IActionCreator<GameCharacter> enemyActionCreator)
        {
            this.enemyActionCreator = enemyActionCreator;
        }

        public List<GoapAction<GameCharacter>> GetEnemyActions()
        {
            return enemyActionCreator.GetActions();
        }

        public void SetEnemyWalkAction(WalkAction<GameCharacter> walkAction)
        {
            enemyWalkAction = walkAction;
        }

        public WalkAction<GameCharacter> GetEnemyWalkAction()
        {
            return enemyWalkAction;
        }

        public void SetPedestrianWalkAction(WalkAction<Pedestrian> walkAction)
        {
            pedestrianWalkAction = walkAction;
        }

        public WalkAction<Pedestrian> GetPedestrianWalkAction()
        {
            return (WalkAction <Pedestrian>) pedestrianWalkAction.Clone();
        }

        //public void Init()
        //{

        //    walkTargetStore.GetTargets().ForEach(target =>
        //    {
        //        var clone = (WalkAction)WalkAction.Clone();
        //        clone.SetTarget(target.gameObject).SetHideDuration(target.hideDuration).SetAfterEffect(new AIState("goto" + target.name, 3));
        //        pedestrianActions.Add(clone);
        //    });
        //}

        public void Reset()
        {
        }
    }
}
