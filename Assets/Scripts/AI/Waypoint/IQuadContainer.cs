using UnityEngine;

namespace AI
{
    public interface IQuadContainer
    {
        GameObject QuadContainer { set; get; }
        WaypointQuad QuadTemplate { set; get; }
    }
}
