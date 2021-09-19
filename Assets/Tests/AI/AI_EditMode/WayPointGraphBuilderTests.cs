using AI;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

public class WaypointGraphBuilderTests
{
    [Test]
    public void WaypointGraphBuilderTestsSimplePasses()
    {
        var waypoint = Substitute.For<IWaypoint>();
        waypoint.Branches.Returns(new List<IWaypoint>());

        var waypoints = new List<IWaypoint> { waypoint };

        var graphBuilder = new WaypointGraphBuilder();
        var graph = new DirectedGraph<IWaypoint, object>();

        graphBuilder.BuildGraph(waypoints, graph);

        Assert.AreEqual(graph.ContainsNode(waypoint), true);
    }
}
