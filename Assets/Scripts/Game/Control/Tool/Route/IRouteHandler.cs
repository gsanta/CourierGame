using UnityEngine;

namespace Routes
{
    public interface IRouteHandler
    {
        LineRenderer InstantiateLinerRenderer();
        void Destroy(LineRenderer lineRenderer);
    }
}
