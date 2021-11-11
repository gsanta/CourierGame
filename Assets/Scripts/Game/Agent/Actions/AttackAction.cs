using AI;
using Scenes;
using Route;
using System.Collections.Generic;
using UnityEngine;

namespace Agents
{
    public class AttackAction<T> : AbstractRouteAction<T> where T : IGameObject
    {
        public AttackAction(AIStateName[] preconditions, AIStateName[] afterEffects,  PathCache pathCache = null) : base(preconditions, afterEffects, pathCache)
        {
        }

        protected override Queue<Vector3> BuildRoute(Vector3 from, Vector3 to)
        {
            return new Queue<Vector3>(new List<Vector3>() { to });
        }

        public override GoapAction<T> Clone(GoapAgent<T> agent = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
