using UnityEngine;
using Zenject;

namespace Movement
{
    public class GridStore
    {
        private GridConfigProvider gridConfigProvider;

        public GridStore(GridConfigProvider gridConfigProvider)
        {
            this.gridConfigProvider = gridConfigProvider;
        }

        private GridNode[,] gridNodes;

        public GridNode GetNodeAt(int x, int y)
        {
            return gridNodes[y, x];
        }

        public void GridInit()
        {
            GridConfig gridConfig = gridConfigProvider.Data;
            Vector3 topLeft = gridConfig.topLeftPoint.transform.position;

            gridNodes = new GridNode[gridConfig.gridRows, gridConfig.gridCols];

            for (int x = 0; x < gridConfig.gridCols; x++)
            {
                for (int z = 0; z < gridConfig.gridRows; z++)
                {
                    float xPos = x * gridConfig.gridSize + topLeft.x;
                    float zPos = z * gridConfig.gridSize + topLeft.z;
                    gridNodes[z, x] = new GridNode(new Vector3(xPos, topLeft.y, zPos), new IntPos(x, z));
                }
            }
        }
    }
}
