using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Movement
{
    public class TileManager : MonoBehaviour
    {
        private bool visible;
        private GridSystem gridSystem;

        public IntPos TopLeft { get; private set; }

        [Inject]
        public void Construct(GridSystem gridSystem)
        {
            this.gridSystem = gridSystem;
            gridSystem.TileManager = this;
        }

        private void Start()
        {
            gridSystem.GridInit();
            //GridConfig gridConfig = gridSystem.GridConfig;

            //tiles = new Tile[gridConfig.tileRows, gridConfig.tileCols];
            //for (int z = 0; z < gridConfig.tileRows; z++)
            //{
            //    for (int x = 0; x < gridConfig.tileCols; x++)
            //    {
            //        Tile tile = Instantiate(gridConfig.tileTemplate, transform);
            //        tiles[z, x] = tile;
            //        tile.InitMesh();
            //    }
            //}

            //UpdateTilePositions(new IntPos(gridConfig.xSectionStart, gridConfig.zSectionStart));
        }

        public Tile CreateTile(Tile tileTemplate)
        {
            Tile tile = Instantiate(tileTemplate, transform);
            tile.InitMesh();
            return tile;
        }

        //public void UpdateTilePositions(IntPos from)
        //{
        //    TopLeft = from;

        //    for (int z = 0; z < tiles.GetLength(0); z++)
        //    {
        //        for (int x = 0; x < tiles.GetLength(1); x++)
        //        {
        //            Tile tile = tiles[z, x];

        //            int gridX = x + from.x;
        //            int gridY = z + from.y;
        //            GridNode gridNode = gridSystem.GetNodeAt(gridX, gridY);

        //            tile.SetCenterPoint(new Vector3(gridNode.Position.x, 1, gridNode.Position.z));
        //        }
        //    }
        //}

        public void UpdateTileVisibility()
        {
            if (!visible) return;

            var gridNodes = gridSystem.gridNodes;

            for (int z = 0; z < gridNodes.GetLength(0); z++)
            {
                for (int x = 0; x < gridNodes.GetLength(1); x++)
                {
                    Tile tile = gridNodes[z, x].Tile;

                    int gridX = x + TopLeft.x;
                    int gridY = z + TopLeft.y;
                    GridNode gridNode = gridSystem.GetNodeAt(gridX, gridY);

                    NavMeshHit hit;
                    bool tileVisible = NavMesh.SamplePosition(new Vector3(gridNode.Position.x, 0, gridNode.Position.z), out hit, 0.5f, NavMesh.AllAreas);
                    tile.SetVisible(tileVisible);
                }
            }
        }

        public Tile GetRandomTile()
        {
            var visibleTiles = GetVisibleTiles();

            return null;

            //int tile = Random.Range(0, visibleTiles.Count - 1);

            //return visibleTiles[tile];
        }

        private List<Tile> GetVisibleTiles()
        {
            List<Tile> visibleTiles = new List<Tile>();
            var gridNodes = gridSystem.gridNodes;

            for (int z = 0; z < gridNodes.GetLength(0); z++)
            {
                for (int x = 0; x < gridNodes.GetLength(1); x++)
                {
                    if (gridNodes[z, x].Tile.IsVisible())
                    {
                        visibleTiles.Add(gridNodes[z, x].Tile);
                    }
                }
            }

            return visibleTiles;
        }

        public void SetVisible(bool visible)
        {
            var gridNodes = gridSystem.gridNodes;

            foreach (var item in gridNodes)
            {
                item.Tile.SetVisible(visible);
            }
            this.visible = visible;
        }
    }
}
