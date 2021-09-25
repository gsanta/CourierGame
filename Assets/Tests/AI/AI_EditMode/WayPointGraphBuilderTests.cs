using AI;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class WaypointGraphBuilderTests
{
    [Test]
    public void WaypointGraphBuilderTestsSimplePasses()
    {
        var waypoint = Substitute.For<Waypoint>();
        waypoint.Branches.Returns(new List<Waypoint>());

        var waypoints = new List<Waypoint> { waypoint };

        var graphBuilder = new WaypointGraphBuilder();
        var graph = new DirectedGraph<Waypoint, object>();

        graphBuilder.BuildGraph(waypoints, graph);

        Assert.AreEqual(graph.ContainsNode(waypoint), true);
    }
}
