

using UnityEngine;

namespace Pedestrians
{
    public class WalkTarget : MonoBehaviour
    {
        [Range(0, 5)]
        public int priority = 0;

        public float hideDuration = 0;
    }
}
