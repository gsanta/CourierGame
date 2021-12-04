
using Cameras;
using Movement;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
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
        private TileManagerProvider tileManagerProvider;
        private GridConfigProvider gridConfigProvider;

        [Inject]
        public void Construct(CameraController cameraController, TileManagerProvider tileManagerProvider, GridConfigProvider gridConfigProvider)
        {
            this.cameraController = cameraController;
            this.tileManagerProvider = tileManagerProvider;
            this.gridConfigProvider = gridConfigProvider;
        }

        private void Awake()
        {
            direction = GetDirection();
        }

        private void Update()
        {
            if (isActive)
            {
                TileManager tileManager = tileManagerProvider.Data;
                GridConfig gridConfig = gridConfigProvider.Data;

                cameraController.Pan(direction);
                IntPos topLeft = tileManager.TopLeft;

                switch(direction)
                {
                    case CameraDirection.UP:
                        topLeft.y += gridConfig.tileRows;
                        break;
                    case CameraDirection.RIGHT:
                        topLeft.x += gridConfig.tileCols;
                        break;
                    case CameraDirection.DOWN:
                        topLeft.y -= gridConfig.tileCols;
                        break;
                    case CameraDirection.LEFT:
                        topLeft.x -= gridConfig.tileCols;
                        break;
                }
                tileManager.UpdateTilePositions(topLeft);
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
