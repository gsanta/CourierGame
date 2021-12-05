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

        private GridNode[,] gridNodes;

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

        private void GridInit()
        {
            GridConfig gridConfig = GridConfig;


            gridNodes = new GridNode[gridConfig.gridRows, gridConfig.gridCols];

            for (int x = 0; x < gridConfig.gridCols; x++)
            {
                for (int z = 0; z < gridConfig.gridRows; z++)
                {
                    gridNodes[z, x] = new GridNode(new IntPos(x, z));
                }
            }
        }

        private void UpdateGridPos(GameObject bottomLeftObj)
        {
            GridConfig gridConfig = GridConfig;
            Vector3 bottomLeft = bottomLeftObj.transform.position;

            gridNodes = new GridNode[gridConfig.gridRows, gridConfig.gridCols];

            for (int x = 0; x < gridConfig.gridCols; x++)
            {
                for (int z = 0; z < gridConfig.gridRows; z++)
                {
                    float xPos = x * gridConfig.gridSize + bottomLeft.x;
                    float zPos = z * gridConfig.gridSize + bottomLeft.z;

                    gridNodes[z, x].Position = new Vector3(xPos, bottomLeft.y, zPos);
                }
            }
        }
    }
}
