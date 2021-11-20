﻿using AI;
using Bikers;
using Routes;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    public class RouteTool : Tool
    {
        private IRouteHandler routeHandler;
        private List<LineRenderer> lineRenderers = new List<LineRenderer>();
        private LineRenderer lineRenderer;
        private List<Vector3> points = new List<Vector3>();
        private readonly BikerStore playerStore;

        private GameObject activeQuad;

        public RouteTool(BikerStore playerStore) : base(ToolName.ROUTE)
        {
            this.playerStore = playerStore;
        }

        public List<Vector3> GetPoints()
        {
            return points;
        }

        public void SetRouteHandler(IRouteHandler routeHandler)
        {
            this.routeHandler = routeHandler;
            CreateLineRenderer();
        }

        public override void Click()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Route");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                var gameObject = hit.transform.gameObject;
                if (points.Count == 0)
                {
                    points.Insert(0, playerStore.GetActivePlayer().transform.position);
                }

                var centerPoint = gameObject.GetComponent<WaypointQuad>().CenterPoint;
                centerPoint.y = playerStore.GetActivePlayer().transform.position.y;

                points.Add(centerPoint);
                UpdateLines();
            }
        }

        public override void Move()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Route");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                var gameObject = hit.transform.gameObject;
                if (activeQuad != null && activeQuad != gameObject)
                {
                    activeQuad.GetComponent<MeshRenderer>().enabled = false;
                }

                activeQuad = gameObject;
                activeQuad.GetComponent<MeshRenderer>().enabled = true;
            } else
            {
                if (activeQuad != null)
                {
                    activeQuad.GetComponent<MeshRenderer>().enabled = false;
                    activeQuad = null;
                }
            }
        }

        public void Reset()
        {
            points = new List<Vector3>();
            lineRenderers.ForEach(lineRenderer => routeHandler.Destroy(lineRenderer));
            lineRenderers = new List<LineRenderer>();
            lineRenderer = null;
            CreateLineRenderer();
        }

        public void Step()
        {
            points = new List<Vector3>();
            lineRenderers.Add(lineRenderer);
            CreateLineRenderer();
        }

        private void CreateLineRenderer()
        {
            lineRenderer = routeHandler.InstantiateLinerRenderer();
            lineRenderer.startWidth = 0.3f;
            lineRenderer.endWidth = 0.3f;
            lineRenderer.useWorldSpace = true;
            lineRenderers.Add(lineRenderer);
        }

        private void UpdateLines()
        {
            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        }
    }
}
