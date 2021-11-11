using UnityEngine;
using Zenject;

namespace Route
{
    public class RouteVisualizer : MonoBehaviour
    {
        private RouteStore routeStore;

        [Inject]
        public void Construct(RouteStore pavementStore)
        {
            this.routeStore = pavementStore;
        }

        private void Start()
        {
            routeStore.GetWaypoints();
        }
    }
}
