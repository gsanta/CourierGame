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

        public void SetCameraConfig(ICameraConfig cameraConfig)
        {
            this.cameraConfig = cameraConfig;
            newPosition = cameraConfig.CameraHandleTransform.position;
            newRotation = cameraConfig.CameraHandleTransform.rotation;
            newZoom = cameraConfig.CameraTransform.localPosition;
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

        public void Zoom(Vector3 zoom)
        {
            newZoom += zoom;
        }

        public void PanTo(Vector3 position)
        {
            newPosition = position;
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
