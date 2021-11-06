﻿using Cameras;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class CameraController : MonoBehaviour
    {
        public Transform cameraTransform;
        public float movementSpeed;
        public float movementTime;
        public float rotationAmount;
        public Vector3 zoomAmount;
        public Vector3 newPosition;
        public Quaternion newRotation;
        public Vector3 newZoom;
        public int boundary = 50;

        private int screenWidth;
        private int screenHeight;

        private void Start()
        {
            newPosition = transform.position;
            newRotation = transform.rotation;
            newZoom = cameraTransform.localPosition;
            screenWidth = Screen.width;
            screenHeight = Screen.height;
        }


        private void Update()
        {
            HandleMovementInput();
            HandleMouseInput();
        }

        private void HandleMovementInput()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                newPosition += transform.forward * movementSpeed;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                newPosition += transform.forward * -movementSpeed;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                newPosition += transform.right * movementSpeed; 
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                newPosition += transform.right * -movementSpeed;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
            }

            if (Input.GetKey(KeyCode.E))
            {
                newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
            }

            if (Input.GetKey(KeyCode.R))
            {
                newZoom += zoomAmount;
            }

            if (Input.GetKey(KeyCode.F))
            {
                newZoom -= zoomAmount;
            }

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
        
        }

        private void HandleMouseInput()
        {
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;

            if (x > screenWidth - boundary && x < screenWidth)
            {
                newPosition += transform.right * movementSpeed;
            } 
            else if (x < boundary && x > 0)
            {
                newPosition += transform.right * -movementSpeed;
            }

            if (y > screenHeight - boundary && y < screenHeight)
            {
                newPosition += transform.forward * movementSpeed;
            }
            else  if (y < boundary && y > 0)
            {
                newPosition += transform.forward * -movementSpeed;
            }
        }
    }
}

