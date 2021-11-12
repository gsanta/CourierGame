using Controls;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class RouteHandler : MonoBehaviour
    {
        private RouteTool routeTool;

        [Inject]
        public void Construct(RouteTool routeTool)
        {
            this.routeTool = routeTool;
            routeTool.SetGameObject(gameObject);
        }
    }
}
