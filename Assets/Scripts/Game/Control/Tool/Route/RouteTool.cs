using UnityEngine;

namespace Controls
{
    public class RouteTool : Tool
    {
        public RouteTool() : base(ToolName.ROUTE)
        {

        }

        public override void Click()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Route");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                var gameObject = hit.transform.gameObject;
            }
        }
    }
}
