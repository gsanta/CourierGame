using UnityEngine;

namespace GUI
{
    public struct SelectionRect
    {
        public readonly Vector2 min;
        public readonly Vector2 max;

        public SelectionRect(Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
        }
    }

    public interface ISelectionBox
    {
        public void StartSelect();
        public void UpdateSelect();
        public SelectionRect EndSelect();
    }
}
