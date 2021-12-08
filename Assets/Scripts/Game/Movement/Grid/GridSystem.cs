using UnityEngine;

namespace Movement
{
    public class GridSystem
    {
        public GridConfig GridConfig
        {
            get;
            set;
        }

        public TileManager TileManager
        {
            get;
            set;
        }

        public GridNode[,] gridNodes;

        public GridNode GetNodeAt(int x, int y)
        {
            return gridNodes[y, x];
        }

        public void SetBottomLeft(GameObject bottomLeft)
        {
            if (gridNodes == null)
            {
                GridInit();
            }

            UpdateGridPos(bottomLeft);
        }

        public void GridInit()
        {
            GridConfig gridConfig = GridConfig;


            gridNodes = new GridNode[gridConfig.gridRows, gridConfig.gridCols];

            for (int x = 0; x < gridConfig.gridCols; x++)
            {
                for (int z = 0; z < gridConfig.gridRows; z++)
                {
                    var gridNode = new GridNode(new IntPos(x, z));
                    Tile tile = TileManager.CreateTile(gridConfig.tileTemplate);
                    tile.SetCenterPoint(new Vector3(gridNode.Position.x, 1, gridNode.Position.z));
                    gridNode.Tile = tile;
                    gridNodes[z, x] = gridNode;
                }
            }

            UpdateGridPos(GridConfig.bottomLeft);
        }

        private void UpdateGridPos(GameObject bottomLeftObj)
        {
            GridConfig gridConfig = GridConfig;
            Vector3 bottomLeft = bottomLeftObj.transform.position;

            for (int x = 0; x < gridConfig.gridCols; x++)
            {
                for (int z = 0; z < gridConfig.gridRows; z++)
                {
                    float xPos = x * gridConfig.gridSize + bottomLeft.x;
                    float zPos = z * gridConfig.gridSize + bottomLeft.z;

                    var gridNode = gridNodes[z, x];
                    gridNode.Position = new Vector3(xPos, bottomLeft.y, zPos);
                    gridNode.Tile.SetCenterPoint(new Vector3(gridNode.Position.x, 1, gridNode.Position.z));
                }
            }
        }
    }
}
