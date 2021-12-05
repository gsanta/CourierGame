using Cameras;
using UnityEngine;
using Zenject;

namespace Movement
{
    public class GridInit : MonoBehaviour
    {
        private GridSystem gridSystem;
        private CameraController cameraController;

        [Inject]
        public void Construct(GridSystem gridSystem, CameraController cameraController)
        {
            this.gridSystem = gridSystem;
            this.cameraController = cameraController;
        }

        private void Awake()
        {
            cameraController.Pan(CameraDirection.LEFT);
            gridSystem.SetBottomLeft(gridSystem.GridConfig.bottomLeft);
        }
    }
}
