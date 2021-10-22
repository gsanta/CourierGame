using Bikers;
using Core;
using Service;
using System;
using UnityEngine;

namespace Cameras
{
    public class CameraHandler : IResetable
    {
        private Vector3 defaultPosition;
        private Quaternion defaultRotation;
        private BikerService bikerService;
        private Biker currentBiker;
        private GameObject camera;

        public CameraHandler(EventService eventService, BikerService bikerService)
        {
            this.bikerService = bikerService;

            eventService.BikerCurrentRoleChanged += HandleCurrentRoleChanged;
        }


        public void SetDefaultCameraPosition(Vector3 defaultPosition, Quaternion defaultRotation)
        {
            this.defaultPosition = defaultPosition;
            this.defaultRotation = defaultRotation;
        }
        public void SetCamera(GameObject camera)
        {
            this.camera = camera;
        }

        public void Reset()
        {
            camera = null;
        }

        public void UpdatePosition()
        {
            if (camera != null && currentBiker != null)
            {
                camera.transform.position = currentBiker.viewPoint.position;
                camera.transform.rotation = currentBiker.viewPoint.rotation;
            }
        }


        public void ResetPosition()
        {
            camera.transform.position = defaultPosition;
            camera.transform.rotation = defaultRotation;
        }

        private void HandleCurrentRoleChanged(object sender, EventArgs args)
        {
            currentBiker = bikerService.FindPlayOrFollowRole();

            if (currentBiker == null)
            {
                ResetPosition();
            }
        }

    }
}
