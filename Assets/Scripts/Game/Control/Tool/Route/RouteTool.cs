using AI;
using Bikers;
using GizmoNS;
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
        private List<Vector3> points = new List<Vector3>();
        private readonly BikerStore playerStore;
        private RoadStore roadStore;
        private ArrowRendererProvider arrowRendererProvider;

        private GameObject activeQuad;
        public int maxRouteLength = 5;
        private bool enabled = true;

        public RouteTool(BikerStore playerStore, RoadStore roadStore, ArrowRendererProvider arrowRendererProvider) : base(ToolName.ROUTE)
        {
            this.playerStore = playerStore;
            this.roadStore = roadStore;
            this.arrowRendererProvider = arrowRendererProvider;
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
            activeQuad = null;
            quad.GetComponent<Renderer>().enabled = false;
            arrowRendererProvider.ArrowRenderer.SetColor(Color.red);
            points.Clear();
        }

        private void HandleQuadEntered(GameObject gameObject)
        {
            activeQuad = gameObject;
            var quad = activeQuad.GetComponent<WaypointQuad>();
            quad.GetComponent<Renderer>().enabled = true;

            var route = roadStore.BuildRoute(playerStore.GetActivePlayer().transform.position, activeQuad.GetComponent<WaypointQuad>().CenterPoint);

            points = route.ToList();
            points.Insert(0, playerStore.GetActivePlayer().transform.position);

            if (points.Count > maxRouteLength)
            {
                arrowRendererProvider.ArrowRenderer.SetColor(Color.yellow);
            } else
            {
                arrowRendererProvider.ArrowRenderer.SetColor(Color.green);
            }
        }

        public void Reset()
        {
            points = new List<Vector3>();
        }

        public void SaveRoute()
        {
            points = new List<Vector3>();
        }

        public void ClearRoute()
        {
            points = new List<Vector3>();
        }
    }
}
