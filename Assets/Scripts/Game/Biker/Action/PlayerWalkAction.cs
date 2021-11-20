﻿using AI;
using Scenes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bikers
{
    public class PlayerWalkAction : AbstractRouteAction<Biker>
    {
        private List<Vector3> points;

        public PlayerWalkAction(GoapAgent<Biker> agent, List<Vector3> points) : base(new AIStateName[] { }, new AIStateName[] { AIStateName.WALK_FINISHED })
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

        public override GoapAction<Biker> Clone(GoapAgent<Biker> agent = null)
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
        public PlayerWalkAction Create(GoapAgent<Biker> agent, List<Vector3> points)
        {
            return new PlayerWalkAction(agent, points);
        }
    }
}
