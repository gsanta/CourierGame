using AI;
using Bikers;
using GameObjects;
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

        private GameObject activeTarget;
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
                
                if (activeTarget != gameObject)
                {
                    if (activeTarget != null)
                    {
                        HandleQuadExited();
                    }

                    HandleQuadEntered(gameObject);
                }
            } 
            else
            {
                if (activeTarget != null)
                {
                    HandleQuadExited();
                }
            }
        }

        private void HandleQuadExited()
        {
            if (activeTarget.tag == "Road Selector")
            {
                var quad = activeTarget.GetComponent<WaypointQuad>();
                quad.GetComponent<Renderer>().enabled = false;
            }
            else if (activeTarget.tag == "Building Selector")
            {
                var outline = activeTarget.GetComponent<Outline>();
                outline.RemoveOutline();
            }
            activeTarget = null;
            arrowRendererProvider.ArrowRenderer.SetColor(Color.red);
            points.Clear();
        }

        private void HandleQuadEntered(GameObject gameObject)
        {
            activeTarget = gameObject;

            Vector3 end = new Vector3(0, 0, 0);

            if (gameObject.tag == "Road Selector")
            {
                var quad = activeTarget.GetComponent<WaypointQuad>();
                quad.GetComponent<Renderer>().enabled = true;
                end = activeTarget.GetComponent<WaypointQuad>().CenterPoint;
            } else if (gameObject.tag == "Building Selector")
            {
                var outline = activeTarget.GetComponent<Outline>();
                outline.SetOutline();
                end = gameObject.transform.position;
            }

            var route = roadStore.BuildRoute(playerStore.GetActivePlayer().transform.position, end);

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
