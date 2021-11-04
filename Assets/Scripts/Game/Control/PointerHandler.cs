
using System.Collections.Generic;

namespace Controls
{
    public class PointerHandler
    {

        //private List<Tool> tools = new List<Tool>();
        //private Tool activeTool;
        private readonly SelectionTool selectionTool;
        private readonly CommandTool commandTool;

        public PointerHandler(SelectionTool selectionTool, CommandTool commandTool)
        {
            this.selectionTool = selectionTool;
            this.commandTool = commandTool;
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
            selectionTool.Down();
        }

        public void PointerUp()
        {
            selectionTool.Up();
        }

        public void PointerClick()
        {
            selectionTool.Click();
        }

        public void PointerRightClick()
        {
            commandTool.RightClick();
        }

        public void PointerDrag()
        {
            selectionTool.Drag();
        }
    }
}
