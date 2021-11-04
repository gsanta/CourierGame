using GameObjects;
using Pedestrians;
using States;
using UnityEngine;

namespace Controls
{
    public class SelectionTool : Tool
    {
        private readonly PedestrianStore pedestrianStore;
        private SelectionBox selectionBox;
        private SelectionStore selectionStore;
        private bool isDragging = false;

        public SelectionTool(SelectionBox selectionBox, PedestrianStore pedestrianStore, SelectionStore selectionStore) : base(ToolName.SELECTION)
        {
            this.selectionBox = selectionBox;
            this.pedestrianStore = pedestrianStore;
            this.selectionStore = selectionStore;
        }

        public override void Click()
        {
            ClearSelect();
            ClickSelect();
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
            ClearSelect();
            RectSelect(selectionRect);
        }

        private void ClearSelect()
        {
            selectionStore.Clear();
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
                    pedestrian.GetGameObjectSelector().Select();
                }
            }
        }

        private void ClickSelect()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector2 hitPoint = new Vector2(hit.point.x, hit.point.y);
                float minDistance = float.MaxValue;
                Pedestrian minDistPedestrian = null;

                foreach (Pedestrian pedestrian in pedestrianStore.GetAll())
                {
                    float distance = Vector2.Distance(hitPoint, new Vector2(pedestrian.transform.position.x, pedestrian.transform.position.y));
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        minDistPedestrian = pedestrian;
                    }
                }

                if (minDistance < 2)
                {
                    var selectableGameObject = minDistPedestrian.GetComponent<ISelectableGameObject>();
                    selectableGameObject.GetGameObjectSelector().Select();
                }
            }
        }
    }
}
