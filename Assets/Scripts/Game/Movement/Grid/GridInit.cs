using Cameras;
using UnityEngine;
using Zenject;

namespace Movement
{
    public class GridInit : MonoBehaviour
    {
        private GridStore gridStore;
        private CameraController cameraController;

        [Inject]
        public void Construct(GridStore gridStore, CameraController cameraController)
        {
            this.gridStore = gridStore;
            this.cameraController = cameraController;
        }

        private void Awake()
        {
            cameraController.Pan(CameraDirection.LEFT);
            gridStore.GridInit();
        }
    }
}
