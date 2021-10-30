using AI;
using Core;
using Route;
using System.Collections.Generic;
using UnityEngine;

namespace Agents
{
    public class AttackAction<T> : AbstractRouteAction<T> where T : IGameObject
    {
        private RouteFacade routeFacade;

        public AttackAction(RouteFacade routeFacade, PathCache pathCache) : base(null, pathCache)
        {
            this.routeFacade = routeFacade;
        }

        protected override Queue<Vector3> BuildRoute(Transform from, Transform to)
        {
            return new Queue<Vector3>(new List<Vector3>() { to.position });
        }

        protected override AIState[] GetAfterEffects()
        {
            throw new System.NotImplementedException();
        }

        protected override AIState[] GetPreConditions()
        {
            throw new System.NotImplementedException();
        }

        public override GoapAction<T> Clone(GoapAgent<T> agent = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
