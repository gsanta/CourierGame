using Pedestrians;
using UnityEngine;

namespace GUI
{
    public class SelectionTool : Tool
    {
        private readonly PedestrianStore pedestrianStore;
        private ISelectionBox selectionBox;
        private bool isDragging = false;

        public SelectionTool(PedestrianStore pedestrianStore) : base(ToolName.SELECTION)
        {
            this.pedestrianStore = pedestrianStore;
        }

        public void SetSelectionBox(ISelectionBox selectionBox)
        {
            this.selectionBox = selectionBox;
        }

        public override void Drag()
        {
            if (!isDragging)
            {
                selectionBox.StartSelect();
                isDragging = true;
            }

            selectionBox.UpdateSelect();
        }

        public override void Up()
        {
            isDragging = false;
            SelectionRect selectionRect = selectionBox.EndSelect();
            RectSelect(selectionRect);
        }

        private void RectSelect(SelectionRect selectionRect)
        {
            var min = selectionRect.min;
            var max = selectionRect.max;

            foreach (Pedestrian pedestrian in pedestrianStore.GetAll())
            {
                Vector3 screenPos = Camera.main.WorldToScreenPoint(pedestrian.transform.position);

                if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
                {
                    pedestrian.GetGameObjectSelector().Deselect();
                }
            }
        }
    }
}
