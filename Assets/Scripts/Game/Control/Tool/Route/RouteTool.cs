using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    public class RouteTool : Tool
    {
        private LineRenderer lineRenderer;
        private List<Vector3> points = new List<Vector3>();

        public RouteTool() : base(ToolName.ROUTE)
        {

        }

        public void SetGameObject(GameObject gameObject)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.3f;
            lineRenderer.endWidth = 0.3f;
            lineRenderer.useWorldSpace = true;
        }

        public override void Click()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Route");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                var gameObject = hit.transform.gameObject;
                points.Add(gameObject.transform.position);
                UpdateLines();
            }
        }

        private void UpdateLines()
        {
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        }
    }
}
