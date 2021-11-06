using Bikers;
using Service;
using UnityEngine;

namespace Cameras
{
    public class CameraConfig
    {
        public Transform cameraTransform;
        public float movementSpeed;
        public float movementTime;
        public float rotationAmount;
    }

    public class CameraController
    {
        private Vector3 defaultPosition;
        private Quaternion defaultRotation;
        private BikerService bikerService;
        private Biker currentBiker;
        private GameObject camera;

        public CameraController(EventService eventService, BikerService bikerService)
        {
            this.bikerService = bikerService;

        }

        public void Pan(CameraDirection cameraDirection)
        {
            switch (cameraDirection)
            {
                case CameraDirection.UP:
                    newPosition += transform.forward * -movementSpeed;
                    break;
                case CameraDirection.DOWN:
                    newPosition += transform.forward * movementSpeed;
                    break;
                case CameraDirection.LEFT:
                    newPosition += transform.right * -movementSpeed;
                    break;
                case CameraDirection.RIGHT:
                    newPosition += transform.right * movementSpeed;
                    break;
            }
        }
    }
}
