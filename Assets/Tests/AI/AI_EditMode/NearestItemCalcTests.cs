using AI;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NearestItemCalcTests
{
    [Test]
    public void Find_Nearest_Item()
    {
        var item = new Vector3(5, 5, 5);
        List<Vector3> items = new List<Vector3>
        {
            new Vector3(6, 6, 5),
            new Vector3(5, 4, 5),
            new Vector3(0, 0, 5)
        };

        NearestItemCalc<Vector3, Vector3> calc = new NearestItemCalc<Vector3, Vector3>(x => x, x => x);

        var nearestItem = calc.GetNearest(item, items);
        Assert.AreEqual(nearestItem, items[1]);
    }
}
