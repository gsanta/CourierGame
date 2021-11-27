using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public interface IRoad<out TNode> where TNode : class, IMonoBehaviour
    {
        Queue<Vector3> BuildRoute(Vector3 from, Vector3 to);
        List<IMonoBehaviour> GetNodes();
    }
}
