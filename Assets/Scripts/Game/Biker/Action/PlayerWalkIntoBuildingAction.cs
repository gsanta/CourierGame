using AI;
using Cameras;
using GamePlay;
using Movement;
using Scenes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjects
{
    public class PlayerWalkIntoBuildingAction : AbstractRouteAction<GameCharacter>
    {
        private List<Vector3> points;
        private CameraController cameraController;
        private IGPostAction postAction;

        public PlayerWalkIntoBuildingAction(GoapAgent<GameCharacter> agent, List<Vector3> points, CameraController cameraController) : base(new AIStateName[] { }, new AIStateName[] { AIStateName.WALK_FINISHED })
        {
            this.agent = agent;
            this.points = points;
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

        public override GoapAction<GameCharacter> Clone(GoapAgent<GameCharacter> agent = null)
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
        private CameraController cameraController;
        public PlayerWalkIntoBuildingActionCreator(CameraController cameraController)
        {
            this.cameraController = cameraController;
        }

        public PlayerWalkIntoBuildingAction Create(GoapAgent<GameCharacter> agent, List<Vector3> points)
        {
            return new PlayerWalkIntoBuildingAction(agent, points, cameraController);
        }
    }
}
