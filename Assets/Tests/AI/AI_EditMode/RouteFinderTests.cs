using AI;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RouteFinderTests
{
    [Test]
    public void RouteFinderTestsSimplePasses()
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

        Assert.AreEqual(5, route.Count);
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
