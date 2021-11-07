using UnityEngine;

namespace Cameras
{
    public interface ICameraConfig
    {
        public Transform CameraTransform { get; }
        public Transform CameraHandleTransform { get; }
        public float MovementSpeed { get; }
        public float TimeFactor { get; }
        public float RotationAmount { get; }
        public Vector3 ZoomAmount { get; }
    }
}
