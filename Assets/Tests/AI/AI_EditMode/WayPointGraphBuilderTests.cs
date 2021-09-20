using AI;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

public class WaypointGraphBuilderTests
{
    [Test]
    public void Builds_A_Graph_Of_Waypoints()
    {
        var waypoints = CreateWaypoints();
        var graphBuilder = new WaypointGraphBuilder();
        var graph = new DirectedGraph<IWaypoint, object>();

        graphBuilder.BuildGraph(waypoints, graph);

        var waypoint1Outgoings = graph.OutgoingNodes(waypoints[0]).ToArray();
        Assert.AreEqual(waypoint1Outgoings.Contains(waypoints[1]), true);
        Assert.AreEqual(1, waypoint1Outgoings.Length);

        var waypoint2Outgoings = graph.OutgoingNodes(waypoints[1]).ToArray();

        Assert.AreEqual(waypoint2Outgoings.Length, 4);
        Assert.AreEqual(waypoint2Outgoings.Contains(waypoints[0]), true);
        Assert.AreEqual(waypoint2Outgoings.Contains(waypoints[2]), true);
        Assert.AreEqual(waypoint2Outgoings.Contains(waypoints[3]), true);
        Assert.AreEqual(waypoint2Outgoings.Contains(waypoints[5]), true);

        var waypoint4Outgoings = graph.OutgoingNodes(waypoints[3]).ToArray();
        Assert.AreEqual(true, waypoint4Outgoings.Contains(waypoints[1]));
        Assert.AreEqual(true, waypoint4Outgoings.Contains(waypoints[4]));
        Assert.AreEqual(2, waypoint4Outgoings.Length);
    }

    private List<IWaypoint> CreateWaypoints()
    {
        var waypoint1 = Substitute.For<IWaypoint>();
        waypoint1.Id.Returns("1");
        waypoint1.Branches.Returns(new List<IWaypoint>());
        waypoint1.PrevWayPoint = null;

        var waypoint2 = Substitute.For<IWaypoint>();
        waypoint2.Id.Returns("2");
        waypoint1.NextWayPoint = waypoint2;
        waypoint2.PrevWayPoint = waypoint1;

        var waypoint3 = Substitute.For<IWaypoint>();
        waypoint3.Id.Returns("3");
        waypoint3.Branches.Returns(new List<IWaypoint>());
        waypoint2.NextWayPoint = waypoint3;
        waypoint3.PrevWayPoint = waypoint2;

        var waypoint4 = Substitute.For<IWaypoint>();
        waypoint4.Id.Returns("4");
        waypoint4.Branches.Returns(new List<IWaypoint> { waypoint2 });
        waypoint4.PrevWayPoint = null;
        waypoint4.NextWayPoint = null;

        var waypoint5 = Substitute.For<IWaypoint>();
        waypoint5.Id.Returns("5");
        waypoint5.Branches.Returns(new List<IWaypoint>());
        waypoint5.PrevWayPoint = waypoint4;
        waypoint4.NextWayPoint = waypoint5;

        var waypoint6 = Substitute.For<IWaypoint>();
        waypoint6.Id.Returns("6");
        waypoint6.Branches.Returns(new List<IWaypoint> { waypoint2 });

        waypoint2.Branches.Returns(new List<IWaypoint> { waypoint4, waypoint6 });

        return new List<IWaypoint> { waypoint1, waypoint2, waypoint3, waypoint4, waypoint5, waypoint6 };
    }
}
