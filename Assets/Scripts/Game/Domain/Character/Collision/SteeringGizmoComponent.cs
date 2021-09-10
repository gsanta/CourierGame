using UnityEngine;

namespace Domain
{
    public class SteeringGizmoComponent : MonoBehaviour
    {
        void OnDrawGizmosSelected()
        {
            Debug.Log("Selected");
        }
    }
}
