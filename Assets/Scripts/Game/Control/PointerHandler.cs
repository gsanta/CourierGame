
namespace Controls
{
    public class PointerHandler
    {

        //private List<Tool> tools = new List<Tool>();
        //private Tool activeTool;
        private readonly SelectionTool selectionTool;
        private readonly CommandTool commandTool;
        private readonly RouteTool routeTool;
        private Tool activeTool;

        public PointerHandler(SelectionTool selectionTool, CommandTool commandTool, RouteTool routeTool)
        {
            this.selectionTool = selectionTool;
            this.commandTool = commandTool;
            this.routeTool = routeTool;
            activeTool = routeTool;
        }

        //public void AddTool(Tool tool)
        //{
        //    tools.Add(tool);
        //}

        //public void SetActiveTool(ToolName name)
        //{
        //    activeTool = tools.Find(tool => tool.name == name);
        //}

        public void PointerDown()
        {
            activeTool.Down();
        }

        public void PointerUp()
        {
            activeTool.Up();
        }

        public void PointerClick()
        {
            activeTool.Click();
        }

        public void PointerRightClick()
        {
            commandTool.RightClick();
        }

        public void PointerDrag()
        {
            activeTool.Drag();
        }

        public void PointerMove()
        {
            activeTool.Move();
        }
    }
}
