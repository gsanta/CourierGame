using Controls;
using Routes;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class RouteHandler : MonoBehaviour, IRouteHandler
    {
        [SerializeField]
        private GameObject routeRenderer;

        [Inject]
        public void Construct(RouteTool routeTool)
        {
            routeTool.SetRouteHandler(this);
        }

        public LineRenderer InstantiateLinerRenderer()
        {
            GameObject clone = Instantiate(routeRenderer, transform);
            clone.SetActive(true);
            return clone.GetComponent<LineRenderer>();
        }
    }
}
