
using AI;
using Bikers;
using Core;
using Enemies;
using Pedestrians;
using System;
using System.Collections.Generic;

namespace Agents
{
    public class ActionStore : IResetable
    {
        private List<GoapAction<Pedestrian>> pedestrianActions = new List<GoapAction<Pedestrian>>();
        private WalkAction<Enemy> enemyWalkAction;
        private WalkAction<Pedestrian> pedestrianWalkAction;
        private GoHomeAction goHomeAction;
        private Dictionary<Type, IActionCreator<IGameObject>> actionCreators = new Dictionary<Type, IActionCreator<IGameObject>>();
        private IActionCreator<Pedestrian> pedestrianActionCreator;
        private IActionCreator<Enemy> enemyActionCreator;

        public PickUpPackageAction PickUpPackageAction { get; set; }

        public void SetPedestrianActionCreator(IActionCreator<Pedestrian> pedestrianActionCreator)
        {
            this.pedestrianActionCreator = pedestrianActionCreator;
        }

        public List<GoapAction<Pedestrian>> GetPedestrianActions()
        {
            return pedestrianActionCreator.GetActions();
        }

        public void SetEnemyActionCreator(IActionCreator<Enemy> enemyActionCreator)
        {
            this.enemyActionCreator = enemyActionCreator;
        }

        public List<GoapAction<Enemy>> GetEnemyActions()
        {
            return enemyActionCreator.GetActions();
        }

        public void SetEnemyWalkAction(WalkAction<Enemy> walkAction)
        {
            enemyWalkAction = walkAction;
        }

        public void SetGoHomeAction(GoHomeAction goHomeAction)
        {
            this.goHomeAction = goHomeAction;
        }

        public GoHomeAction GetGoHomeAction()
        {
            return goHomeAction;
        }

        public WalkAction<Enemy> GetEnemyWalkAction()
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
