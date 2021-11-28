using AI;
using Cameras;
using GamePlay;
using Movement;
using Scenes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bikers
{
    public class PlayerWalkIntoBuildingAction : AbstractRouteAction<Player>
    {
        private List<Vector3> points;
        private SceneManagerHolder sceneManagerHolder;
        private GridStore gridStore;
        private CameraController cameraController;
        private IGPostAction postAction;

        public PlayerWalkIntoBuildingAction(GoapAgent<Player> agent, List<Vector3> points, SceneManagerHolder sceneManagerHolder, GridStore gridStore, CameraController cameraController) : base(new AIStateName[] { }, new AIStateName[] { AIStateName.WALK_FINISHED })
        {
            this.agent = agent;
            this.points = points;
            this.sceneManagerHolder = sceneManagerHolder;
            this.gridStore = gridStore;
            this.cameraController = cameraController;
        }

        public void AddPostAction(IGPostAction postAction)
        {
            this.postAction = postAction;
        }

        public override bool PrePerform()
        {
            StartRoute(Vector3.zero, Vector3.zero);

            return true;
        }
        public override bool PostPerform()
        {
            agent.Active = false;

            if (postAction != null)
            {
                postAction.Execute();
            }

            return true;
        }

        public override bool PostAbort()
        {
            return true;
        }

        public override GoapAction<Player> Clone(GoapAgent<Player> agent = null)
        {
            throw new Exception("Unimplemented method");
        }

        protected override Queue<Vector3> BuildRoute(Vector3 from, Vector3 to)
        {
            return new Queue<Vector3>(points); ;
        }
    }


    public class PlayerWalkIntoBuildingActionCreator
    {
        private GridStore gridStore;
        private CameraController cameraController;
        public PlayerWalkIntoBuildingActionCreator(GridStore gridStore, CameraController cameraController)
        {
            this.gridStore = gridStore;
            this.cameraController = cameraController;
        }

        public PlayerWalkIntoBuildingAction Create(GoapAgent<Player> agent, List<Vector3> points, SceneManagerHolder sceneManagerHolder)
        {
            return new PlayerWalkIntoBuildingAction(agent, points, sceneManagerHolder, gridStore, cameraController);
        }
    }
}
