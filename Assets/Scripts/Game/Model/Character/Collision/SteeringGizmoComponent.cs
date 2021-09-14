using UnityEngine;

namespace Model
{
    public class SteeringGizmoComponent : MonoBehaviour
    {
        void OnDrawGizmosSelected()
        {
            Debug.Log("Selected");
        }
    }
}
