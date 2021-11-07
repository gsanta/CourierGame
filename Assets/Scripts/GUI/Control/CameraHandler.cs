using Cameras;
using UnityEngine;
using Zenject;

namespace Controls
{

    public class CameraHandler : MonoBehaviour, ICameraConfig
    {
        public Transform cameraTransform;
        public float movementSpeed;
        public float timeFactor;
        public float rotationAmount;
        public Vector3 zoomAmount;
        private CameraController cameraController;
        private Vector3 rotateStartPosition;
        private Vector3 rotateCurrentPosition;

        [Inject]
        public void Construct(CameraController cameraController)
        {
            this.cameraController = cameraController;
        }

        public Transform CameraTransform { get => cameraTransform; }
        public float MovementSpeed { get => movementSpeed; }
        public float TimeFactor { get => timeFactor; }
        public float RotationAmount { get => rotationAmount; }
        public Vector3 ZoomAmount { get => zoomAmount; }

        public Transform CameraHandleTransform => transform;

        private void Awake()
        {
            cameraController.SetCameraConfig(this);
        }

        private void Update()
        {
            HandleZoom();
            HandleRotate();
            cameraController.Update();
        }

        private void HandleZoom()
        {
            if (Input.mouseScrollDelta.y != 0)
            {
                cameraController.Zoom(Input.mouseScrollDelta.y * ZoomAmount);
            }
        }

        private void HandleRotate()
        {
            if (Input.GetMouseButtonDown(2)) {
                rotateStartPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(2))
            {
                rotateCurrentPosition = Input.mousePosition;

                Vector3 difference = rotateStartPosition - rotateCurrentPosition;

                rotateStartPosition = rotateCurrentPosition;

                Quaternion newRotation = Quaternion.Euler(Vector3.up * (-difference.x / 5f));
                
                cameraController.Rotate(newRotation);
            }

        }
    }
}

