using AI;
using Scenes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjects
{
    public class PlayerWalkAction : AbstractRouteAction<GameCharacter>
    {
        private List<Vector3> points;

        public PlayerWalkAction(GoapAgent<GameCharacter> agent, List<Vector3> points) : base(new AIStateName[] { }, new AIStateName[] { AIStateName.WALK_FINISHED })
        {
            this.agent = agent;
            this.points = points;
        }

        public override bool PrePerform()
        {
            StartRoute(Vector3.zero, Vector3.zero);

            return true;
        }
        public override bool PostPerform()
        {
            return true;
        }

        public override bool PostAbort()
        {
            return true;
        }

        public override GoapAction<GameCharacter> Clone(GoapAgent<GameCharacter> agent = null)
        {
            throw new Exception("Unimplemented method");
        }

        protected override Queue<Vector3> BuildRoute(Vector3 from, Vector3 to)
        {
            return new Queue<Vector3>(points);;
        }
    }

    public class PlayerWalkActionCreator
    {
        public PlayerWalkAction Create(GoapAgent<GameCharacter> agent, List<Vector3> points)
        {
            return new PlayerWalkAction(agent, points);
        }
    }
}
