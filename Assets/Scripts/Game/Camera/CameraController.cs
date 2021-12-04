using AI;
using System;
using UnityEngine;

namespace Cameras
{
    public enum CameraDirection
    {
        UP, RIGHT, DOWN, LEFT
    }

    public class CameraController
    {
        private ICameraConfig cameraConfig;
        public Vector3 newPosition;
        public Quaternion newRotation;
        public Vector3 newZoom;
        private IGameObject targetGameObject;

        private GameObject[,] cameraPositionMap;
        private int xCamPos = 0;
        private int yCamPos = 0;

        public void SetCameraConfig(ICameraConfig cameraConfig)
        {
            this.cameraConfig = cameraConfig;
            newPosition = cameraConfig.CameraHandleTransform.position;
            newRotation = cameraConfig.CameraHandleTransform.rotation;
            newZoom = cameraConfig.CameraTransform.localPosition;
        }

        public void SetCameraPositions(GameObject[] cameraPositions, int xDim, int yDim)
        {
            cameraPositionMap = new GameObject[xDim, yDim];

            for (int i = 0; i < yDim; i++)
            {
                for (int j = 0; j < xDim; j++)
                {
                    cameraPositionMap[i, j] = cameraPositions[i * yDim + j];
                }
            }
        }

        public void Update()
        {
            Transform transform = cameraConfig.CameraHandleTransform;
            Transform cameraTransform = cameraConfig.CameraTransform;
            float movementTime = cameraConfig.TimeFactor;

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
        }

        public void Rotate(Quaternion rotation)
        {
            newRotation *= rotation;
        }

        public void PanToDirection(CameraDirection cameraDirection)
        {
            switch(cameraDirection)
            {
                case CameraDirection.UP:
                    if (yCamPos > 0) yCamPos = yCamPos - 1;
                    break;
                case CameraDirection.RIGHT:
                    if (xCamPos < cameraPositionMap.GetLength(1) - 1) xCamPos = xCamPos + 1;
                    break;
                case CameraDirection.DOWN:
                    if (yCamPos < cameraPositionMap.GetLength(0) - 1) yCamPos = yCamPos + 1;
                    break;
                case CameraDirection.LEFT:
                    if (xCamPos > 0) xCamPos = xCamPos - 1;
                    break;
            }

            newPosition = cameraPositionMap[yCamPos, xCamPos].transform.position;
        }

        public void Zoom(Vector3 zoom)
        {
            newZoom += zoom;
        }

        public void PanTo(IGameObject gameObject)
        {
            newPosition = gameObject.GetGameObject().transform.position;
        }

        public void PanTo(GameObject gameObject)
        {
            newPosition = gameObject.transform.position;
        }

        public void Follow(IGameObject targetGameObject)
        {
            //if (this.targetGameObject != null)
            //{
            //    this.targetGameObject.Updated -= UpdateFollow;
            //    this.targetGameObject = null;
            //} 

            //if (targetGameObject != null)
            //{
            //    this.targetGameObject = targetGameObject;
            //    targetGameObject.Updated += UpdateFollow;
            //}
        }

        private void UpdateFollow(object sender, EventArgs eventArgs)
        {
            newPosition = targetGameObject.GetGameObject().transform.position;
        }

        public void Pan(CameraDirection cameraDirection)
        {
            Transform transform = cameraConfig.CameraHandleTransform;
            float movementSpeed = cameraConfig.MovementSpeed;

            switch (cameraDirection)
            {
                case CameraDirection.UP:
                    newPosition += transform.forward * movementSpeed;
                    break;
                case CameraDirection.DOWN:
                    newPosition += transform.forward * -movementSpeed;
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
