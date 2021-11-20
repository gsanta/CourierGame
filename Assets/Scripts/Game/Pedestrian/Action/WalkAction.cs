using Agents;
using AI;
using Enemies;
using Route;
using Scenes;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Pedestrians
{
    public class WalkAction<T> : AbstractRouteAction<T> where T : IGameObject
    {
        private WalkTargetStore walkTargetStore;
        private RoadStore routeStore;
        private float hideDuration = 0;

        public WalkAction(GoapAgent<T> agent, AIStateName[] preconditions, AIStateName[] afterEffects, RoadStore routeStore, WalkTargetStore walkTargetStore, PathCache pathCache) : base(preconditions, afterEffects, pathCache)
        {
            this.agent = agent;
            this.routeStore = routeStore;
            this.walkTargetStore = walkTargetStore;
        }

        public WalkAction<T> SetHideDuration(float duration)
        {
            hideDuration = duration;
            return this;
        }

        public override bool PrePerform()
        {
            T agent = GoapAgent.Parent;
            agent.GetNavMeshAgent().speed = 2;
            Goal goal = GoapAgent.GetCurrentGoal();

            var from = agent.GetGameObject().transform;
            var to = (Vector3) goal.target;

            StartRoute(from.position, to);

            return true;
        }
        public override bool PostPerform()
        {
            var goals = agent.Parent.GetGoalProvider().CreateGoal();
            agent.SetGoals(goals, false);
            return true;
        }

        public override bool PostAbort()
        {
            agent.NavMeshAgent.isStopped = true;
            return true;
        }

        public override GoapAction<T> Clone(GoapAgent<T> agent = null)
        {
            var action = new WalkAction<T>(GoapAgent, GetPreConditions(), GetAfterEffects(), routeStore, walkTargetStore, pathCache);
            action.agent = agent;
            action.hideDuration = hideDuration;
            return action;
        }

        protected override Queue<Vector3> BuildRoute(Vector3 from, Vector3 to)
        {
            return new Queue<Vector3>(new List<Vector3>() { to });
        }
    }

    public class PedestrianWalkActionCreator
    {
        private readonly RoadStore routeStore;
        private readonly WalkTargetStore walkTargetStore;

        public PedestrianWalkActionCreator(RoadStore routeStore, WalkTargetStore walkTargetStore)
        {
            this.routeStore = routeStore;
            this.walkTargetStore = walkTargetStore;
        }

        public WalkAction<Pedestrian> Create(GoapAgent<Pedestrian> agent, AIStateName[] preconditions, AIStateName[] afterEffects)
        {
            return new WalkAction<Pedestrian>(agent, preconditions, afterEffects, routeStore, walkTargetStore, null);
        }
    }

    public class EnemyWalkActionCreator
    {
        private readonly RoadStore routeStore;
        private readonly WalkTargetStore walkTargetStore;

        public EnemyWalkActionCreator(RoadStore routeStore, WalkTargetStore walkTargetStore)
        {
            this.routeStore = routeStore;
            this.walkTargetStore = walkTargetStore;
        }

        public WalkAction<Enemy> Create(GoapAgent<Enemy> agent, AIStateName[] preconditions, AIStateName[] afterEffects)
        {
            return new WalkAction<Enemy>(agent, preconditions, afterEffects, routeStore, walkTargetStore, null);
        }
    }
}

//new WalkAction<Enemy>(new AIStateName[] { }, new AIStateName[] { AIStateName.DESTINATION_REACHED }, pavementStore, walkTargetStore, pathCache),
