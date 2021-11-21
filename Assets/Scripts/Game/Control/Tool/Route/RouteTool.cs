using AI;
using Bikers;
using GamePlay;
using Route;
using Routes;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private RoadStore roadStore;
        private GameObject activeQuad;

        public int maxRouteLength = 5;
        private bool enabled = true;

        public RouteTool(BikerStore playerStore, RoadStore roadStore) : base(ToolName.ROUTE)
        {
            this.playerStore = playerStore;
            this.roadStore = roadStore;
        }

        public event EventHandler RouteFinished;

        public List<Vector3> GetPoints()
        {
            return points;
        }

        public bool Enabled { 
            set
            {
                enabled = value;

                if (!enabled)
                {
                    ClearRoute();
                }
            }
            get => enabled;
        }

        public bool IsValidRoute()
        {
            return points.Count <= maxRouteLength;
        }

        public void SetRouteHandler(IRouteHandler routeHandler)
        {
            this.routeHandler = routeHandler;
            CreateLineRenderer();
        }

        public override void Click()
        {
            if (!enabled)
                return;

            RouteFinished?.Invoke(this, EventArgs.Empty);
        }

        public override void Move()
        {
            if (!enabled)
                return;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask("Route");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                var gameObject = hit.transform.gameObject;
                
                if (activeQuad != gameObject)
                {
                    if (activeQuad != null)
                    {
                        HandleQuadExited();
                    }

                    HandleQuadEntered(gameObject);
                }
            } 
            else
            {
                if (activeQuad != null)
                {
                    HandleQuadExited();
                }
            }
        }

        private void HandleQuadExited()
        {
            var quad = activeQuad.GetComponent<WaypointQuad>();
            quad.Hide();
            activeQuad = null;
            points.Clear();
            UpdateLines();
        }

        private void HandleQuadEntered(GameObject gameObject)
        {
            activeQuad = gameObject;
            var quad = activeQuad.GetComponent<WaypointQuad>();

            var route = roadStore.BuildRoute(playerStore.GetActivePlayer().transform.position, activeQuad.GetComponent<WaypointQuad>().CenterPoint);

            points = route.ToList();
            points.Insert(0, playerStore.GetActivePlayer().transform.position);

            if (points.Count > maxRouteLength)
            {
                quad.SetNotAllowedColor();
            } else
            {
                quad.SetAllowedColor();
            }

            UpdateLines();
        }

        public void Reset()
        {
            points = new List<Vector3>();
            lineRenderers.ForEach(lineRenderer => routeHandler.Destroy(lineRenderer));
            lineRenderers = new List<LineRenderer>();
            lineRenderer = null;
            CreateLineRenderer();
        }

        public void SaveRoute()
        {
            points = new List<Vector3>();
            lineRenderers.Add(lineRenderer);
            CreateLineRenderer();
        }

        public void ClearRoute()
        {
            points = new List<Vector3>();
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
