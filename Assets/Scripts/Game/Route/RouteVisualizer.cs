using UnityEngine;
using Zenject;

namespace Route
{
    public class RouteVisualizer : MonoBehaviour
    {
        private RoadStore routeStore;

        [Inject]
        public void Construct(RoadStore pavementStore)
        {
            this.routeStore = pavementStore;
        }

        private void Start()
        {
            routeStore.GetWaypoints();
        }
    }
}
