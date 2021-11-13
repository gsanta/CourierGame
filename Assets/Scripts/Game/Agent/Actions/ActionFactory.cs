﻿using AI;
using Bikers;
using Route;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Actions
{
    public class ActionFactory
    {
        private readonly RoadStore routeStore;
        public ActionCreators actionCreators = new ActionCreators();

        public ActionFactory([Inject(Id = "PavementStore")] RoadStore routeStore)
        {
            this.routeStore = routeStore;
        }

        public PlayerWalkAction CreatePlayerWalkAction(GoapAgent<Biker> agent, List<Vector3> points)
        {
            return actionCreators.PlayerWalkActionCreator.Create(agent, points, routeStore);
        }
    }
}
