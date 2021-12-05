
using Cameras;
using Movement;
using System;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class PanHandler : MonoBehaviour
    {
        [SerializeField]
        private string directionStr;
        private CameraController cameraController;
        private bool isActive = false;
        private CameraDirection direction;
        private GridSystem gridSystem;

        [Inject]
        public void Construct(CameraController cameraController, GridSystem gridSystem)
        {
            this.cameraController = cameraController;
            this.gridSystem = gridSystem;
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
                IntPos topLeft = gridSystem.TileManager.TopLeft;

                switch(direction)
                {
                    case CameraDirection.UP:
                        topLeft.y += gridSystem.GridConfig.tileRows;
                        break;
                    case CameraDirection.RIGHT:
                        topLeft.x += gridSystem.GridConfig.tileCols;
                        break;
                    case CameraDirection.DOWN:
                        topLeft.y -= gridSystem.GridConfig.tileCols;
                        break;
                    case CameraDirection.LEFT:
                        topLeft.x -= gridSystem.GridConfig.tileCols;
                        break;
                }
                gridSystem.TileManager.UpdateTilePositions(topLeft);
            }
        }

        public void OnClick()
        {
            cameraController.PanToDirection(GetDirection());
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
