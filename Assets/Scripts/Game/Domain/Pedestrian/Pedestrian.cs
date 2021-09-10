using UnityEngine;
using Zenject;

namespace Domain
{
    public class Pedestrian : MonoBehaviour
    {

        public float seeAhead = 5f;


        public class Factory : PlaceholderFactory<UnityEngine.Object, Pedestrian>
        {
        }
    }
}
