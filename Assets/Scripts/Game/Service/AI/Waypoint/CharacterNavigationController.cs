using UnityEngine;

namespace Service.AI
{
    public class CharacterNavigationController : MonoBehaviour
    {
        public float movementSpeed = 1f;
        public float rotationSpeed = 120f;
        public float stopDistance = 1.5f;
        public Vector3 destination;
        public bool reachedDestination = false;

        void Update()
        {
            if (transform.position != destination)
            {
                Vector3 destinationDirection = destination - transform.position;
                destinationDirection.y = 0;

                float destinationDistnace = destinationDirection.magnitude;

                if (destinationDistnace >= stopDistance)
                {
                    reachedDestination = false;
                    Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
                }
                else
                {
                    reachedDestination = true;
                }
            }
        }

        public void SetDestination(Vector3 destination)
        {
            this.destination = destination;
            reachedDestination = false;
        }
    }
}