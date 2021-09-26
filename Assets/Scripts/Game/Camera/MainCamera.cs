using Bikers;
using Model;
using System;
using UnityEngine;
using Zenject;

namespace Cameras
{
    public class MainCamera : MonoBehaviour
    {
        private Vector3 defaultPosition;
        private Quaternion defaultRotation;
        private BikerService bikerService;
        private Biker currentBiker;


        [Inject]
        public void Construct(IEventService eventService, BikerService bikerService)
        {
            this.bikerService = bikerService;

            eventService.BikerCurrentRoleChanged += HandleCurrentRoleChanged;
        }

        private void Start()
        {
            defaultPosition = transform.position;
            defaultRotation = transform.rotation;
        }

        private void Update()
        {
            if (currentBiker != null)
            {
                transform.position = currentBiker.viewPoint.position;
                transform.rotation = currentBiker.viewPoint.rotation;
            }
        }

        public void ResetPosition()
        {
            transform.position = defaultPosition;
            transform.rotation = defaultRotation;
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

