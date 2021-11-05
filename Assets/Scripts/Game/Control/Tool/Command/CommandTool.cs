
using Pedestrians;
using States;
using UnityEngine;

namespace Controls
{
    public class CommandTool : Tool
    {
        private SelectionStore selectionStore;
        private PedestrianGoalFactory pedestrianGoalFactory;
        public CommandTool(SelectionStore selectionStore, PedestrianGoalFactory pedestrianGoalFactory) : base(ToolName.COMMAND)
        {
            this.selectionStore = selectionStore;
            this.pedestrianGoalFactory = pedestrianGoalFactory;
        }

        public override void RightClick()
        {
            Vector2 position = Input.mousePosition;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //hit.point
                selectionStore.GetPedestrians().ForEach(pedestrian => pedestrianGoalFactory.GoToPosition(pedestrian, hit.point));
            }
        }
    }
}
