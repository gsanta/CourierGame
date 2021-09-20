using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public interface IWaypoint
    {
        string Id { get; }
        Vector3 Position { get; }
        IWaypoint PrevWayPoint { get; set; }
        IWaypoint NextWayPoint { get; set; }
        Vector3 Right { get; }
        Vector3 Forward { get; }
        float Width { get; }
        List<IWaypoint> Branches { get; }
        float LeftMargin { get; }
        float RightMargin { get; }
    }
}
