
using UnityEngine;

namespace Movement
{
    public struct IntPos
    {
        public int x;
        public int y;

        public IntPos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public struct GridNode
    {
        public Vector3 Position { get; set; }
        public IntPos GridPos { get; private set; }

        public GridNode(IntPos gridPos)
        {
            Position = new Vector3(0, 0, 0);
            GridPos = gridPos;
        }
    }
}
