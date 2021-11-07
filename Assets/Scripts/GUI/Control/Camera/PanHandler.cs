
using Cameras;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace GUI
{
    public class PanHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private string directionStr;
        private CameraController cameraController;
        private bool isActive = false;
        private CameraDirection direction;

        [Inject]
        public void Construct(CameraController cameraController)
        {
            this.cameraController = cameraController;
        }

        private void Awake()
        {
            direction = GetDirection();
        }

        private void Update()
        {
            if (isActive)
            {
                cameraController.Pan(direction);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isActive = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isActive = false;
        }

        private CameraDirection GetDirection()
        {
            switch(directionStr)
            {
                case "up":
                    return CameraDirection.UP;
                case "right":
                    return CameraDirection.RIGHT;
                case "down":
                    return CameraDirection.DOWN;
                case "left":
                    return CameraDirection.LEFT;
            }
            throw new Exception("wrong direction");
        }
    }
}
