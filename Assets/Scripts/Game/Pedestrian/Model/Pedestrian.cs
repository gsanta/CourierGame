using UnityEngine;
using Zenject;

namespace Pedestrians
{
    public class Pedestrian : MonoBehaviour
    {
        public float seeAhead = 5f;

        private void Update()
        {
            Debug.Log("pedestrian update");
        }

        public class Factory : PlaceholderFactory<Object, Pedestrian>
        {
        }
    }
}
