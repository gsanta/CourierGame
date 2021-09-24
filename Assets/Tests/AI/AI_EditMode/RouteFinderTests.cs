using AI;
using NUnit.Framework;
using System.Collections.Generic;

public class RouteFinderTests
{
    [Test]
    public void Simple_Route()
    {

        var graphBuilder = new GraphBuilder(
            new int[8, 8]
            {
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 1, 0, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 }
            }
        );

        var (graph, nodes) = graphBuilder.GetGraph();
        var routeFinder = new RouteFinder<IntVec, object>(graph, new TestScorer(), new TestScorer());
        var route = routeFinder.FindRoute(nodes[(1, 3)], nodes[(4, 2)]);

        Assert.That(route, Is.EquivalentTo(new List<IntVec> { new IntVec(1, 3), new IntVec(2, 3), new IntVec(3, 3), new IntVec(4, 3), new IntVec(4, 2) }));
    }

    [Test]
    public void Complex_Route()
    {

        var graphBuilder = new GraphBuilder(
            new int[8, 8]
            {
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 1, 0, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0, 0 },
                { 0, 1, 0, 1, 0, 0, 0, 0 },
                { 0, 1, 0, 1, 0, 0, 0, 0 },
                { 0, 1, 1, 1, 1, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0 }
            }
        );

        var (graph, nodes) = graphBuilder.GetGraph();
        var routeFinder = new RouteFinder<IntVec, object>(graph, new TestScorer(), new TestScorer());
        var route = routeFinder.FindRoute(nodes[(2, 6)], nodes[(3, 2)]);

        Assert.That(route, Is.EquivalentTo(new List<IntVec> { new IntVec(2, 6), new IntVec(3, 6), new IntVec(3, 5), new IntVec(3, 4), new IntVec(3, 3), new IntVec(3, 2) }));
    }
}

class IntVec
{
    public int x;
    public int y;

    public IntVec(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return x + ":" + y;
    }

    public override bool Equals(object obj)
    {
        return this.x == (obj as IntVec).x && this.y == (obj as IntVec).y;
    }

    public override int GetHashCode()
    {
        int hashCode = 1502939027;
        hashCode = hashCode * -1521134295 + x.GetHashCode();
        hashCode = hashCode * -1521134295 + y.GetHashCode();
        return hashCode;
    }
}

class GraphBuilder
{
    private readonly int[,] matrix;
    private Dictionary<(int x, int y), IntVec> nodes;

    public GraphBuilder(int[,] matrix)
    {
        this.matrix = matrix;
        nodes = new Dictionary<(int x, int y), IntVec>();
    }

    internal (DirectedGraph<IntVec, object>, Dictionary<(int x, int y), IntVec>) GetGraph()
    {
        DirectedGraph<IntVec, object> graph = new DirectedGraph<IntVec, object>();
        CreateNodes(graph);
        CreateConnections(graph);
        return (graph, nodes);
    }

    private void CreateNodes(DirectedGraph<IntVec, object> graph)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == 1)
                {
                    nodes.Add((j, i), new IntVec(j, i));
                }
            }
        }
    }

    private void CreateConnections(DirectedGraph<IntVec, object> graph)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == 1)
                {
                    LeftConnection(j, i, graph);
                    RightConnection(j, i, graph);
                    TopConnection(j, i, graph);
                    BottomConnection(j, i, graph);
                }
            }
        }
    }

    private void RightConnection(int x, int y, DirectedGraph<IntVec, object> graph)
    {
        if (matrix.GetLength(1) - 1 > x)
        {
            if (matrix[y, x + 1] == 1)
            {
                graph.AddEdge(nodes[(x, y)], nodes[(x + 1, y)], null);
            }
        }
    }

    private void LeftConnection(int x, int y, DirectedGraph<IntVec, object> graph)
    {
        if (x > 0)
        {
            if (matrix[y, x - 1] == 1)
            {
                graph.AddEdge(nodes[(x, y)], nodes[(x - 1, y)], null);
            }
        }
    }

    private void TopConnection(int x, int y, DirectedGraph<IntVec, object> graph)
    {
        if (y > 0)
        {
            if (matrix[y - 1, x] == 1)
            {
                graph.AddEdge(nodes[(x, y)], nodes[(x, y - 1)], null);
            }
        }
    }

    private void BottomConnection(int x, int y, DirectedGraph<IntVec, object> graph)
    {
        if (y < matrix.GetLength(0) - 1)
        {
            if (matrix[y + 1, x] == 1)
            {
                graph.AddEdge(nodes[(x, y)], nodes[(x, y + 1)], null);
            }
        }
    }
}
