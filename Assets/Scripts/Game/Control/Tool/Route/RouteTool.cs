using AI;
using GameObjects;
using GameObjects;
using GizmoNS;
using Movement;
using Route;
using Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Worlds;

namespace Controls
{
    public class RouteTool : Tool
    {
        private readonly PlayerStore playerStore;
        private RoadStore roadStore;
        private ArrowRendererProvider arrowRendererProvider;
        private WorldStore worldStore;
        private GridSystem gridSystem;
        private List<Vector3> points = new List<Vector3>();
        private string tag;

        private GameObject activeTarget;
        public int maxRouteLength = 5;
        private bool enabled = true;


        public RouteTool(PlayerStore playerStore, RoadStore roadStore, ArrowRendererProvider arrowRendererProvider, WorldStore worldStore, GridSystem gridSystem) : base(ToolName.ROUTE)
        {
            this.playerStore = playerStore;
            this.roadStore = roadStore;
            this.arrowRendererProvider = arrowRendererProvider;
            this.worldStore = worldStore;
            this.gridSystem = gridSystem;
        }

        public event EventHandler RouteFinished;

        public List<Vector3> GetPoints()
        {
            return points;
        }

        public string GetTag()
        {
            return tag;
        }

        public bool Enabled { 
            set
            {
                enabled = value;

                if (!enabled)
                {
                    ClearRoute();
                    gridSystem.TileManager.SetVisible(false);
                } else
                {
                    gridSystem.TileManager.SetVisible(true);
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
            else if (TagManager.IsHoverable(activeTarget.tag))
            {
                var outline = activeTarget.GetComponent<Outline>();
                outline.RemoveOutline();
            } else if (TagManager.IsGrid(activeTarget.tag))
            {
                Tile tile = activeTarget.GetComponent<Tile>();
                tile.UnHover();
            }
            activeTarget = null;
            arrowRendererProvider.ArrowRenderer.SetColor(Color.red);
            points.Clear();
        }

        private void HandleQuadEntered(GameObject gameObject)
        {
            activeTarget = gameObject;

            Vector3 end = new Vector3(0, 0, 0);

            //if (gameObject.tag == "Road Selector")
            //{
            //    var quad = activeTarget.GetComponent<WaypointQuad>();
            //    quad.GetComponent<Renderer>().enabled = true;
            //    end = activeTarget.GetComponent<WaypointQuad>().CenterPoint;
            //} else 
            Debug.Log(activeTarget.tag);
            if (TagManager.IsHoverable(activeTarget.tag))
            {
                var outline = activeTarget.GetComponent<Outline>();
                outline.SetOutline();
                end = gameObject.transform.position;
            } else if (gameObject.tag == "Grid")
            {
                Tile tile = gameObject.GetComponent<Tile>();
                end = tile.GetCenterPoint();
                tile.Hover();
            }

            tag = gameObject.tag;

            Queue<Vector3> route = null;
            route = new Queue<Vector3>(new List<Vector3> { end });
            //if (worldStore.CurrentMap == "Building")
            //{
            //} else
            //{
            //    route = roadStore.GetRoad(worldStore.CurrentMap).BuildRoute(playerStore.GetActivePlayer().transform.position, end);
            //}


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
