using AI;

namespace Movement
{
    public class GridGraphBuilder
    {
        public void BuildGraph(GridNode[,] gridNodes, DirectedGraph<GridNode, object> graph)
        {
            int yMax = gridNodes.GetLength(0);
            int xMax = gridNodes.GetLength(1);

            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    if (x > 0)
                    {
                        graph.AddEdge(gridNodes[y, x], gridNodes[y, x - 1], null);
                    }
                    if (x < xMax - 1)
                    {
                        graph.AddEdge(gridNodes[y, x], gridNodes[y, x + 1], null);
                    }
                    if (y > 0)
                    {
                        graph.AddEdge(gridNodes[y, x], gridNodes[y - 1, x], null);
                    }
                    if (y < yMax - 1)
                    {
                        graph.AddEdge(gridNodes[y, x], gridNodes[y + 1, x], null);
                    }
                }
            }
        }
    }
}
