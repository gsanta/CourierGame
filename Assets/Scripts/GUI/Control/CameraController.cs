using Cameras;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class CameraController : MonoBehaviour
    {

        private CameraHandler cameraHandler;

        [Inject]
        public void Construct(CameraHandler cameraHandler)
        {
            this.cameraHandler = cameraHandler;
        }

        private void Awake()
        {
            cameraHandler.SetCamera(this.gameObject);
        }

        private void Start()
        {
            cameraHandler.SetDefaultCameraPosition(transform.position, transform.rotation);
        }

        private void Update()
        {
            cameraHandler.UpdatePosition();
        }
    }
}

