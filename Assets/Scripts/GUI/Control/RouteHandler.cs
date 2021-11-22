using Controls;
using Routes;
using UnityEngine;

namespace GUI
{
    public class RouteHandler : MonoBehaviour, IRouteHandler
    {
        [SerializeField]
        private GameObject routeRenderer;

        public LineRenderer InstantiateLinerRenderer()
        {
            GameObject clone = Instantiate(routeRenderer, transform);
            clone.SetActive(true);
            return clone.GetComponent<LineRenderer>();
        }

        public void Destroy(LineRenderer lineRenderer)
        {
            Destroy(lineRenderer.gameObject);
        }
    }
}
