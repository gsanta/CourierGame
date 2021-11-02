
using System.Collections.Generic;

namespace GUI
{
    public class PointerHandler
    {

        private List<Tool> tools = new List<Tool>();
        private Tool activeTool;

        public void AddTool(Tool tool)
        {
            tools.Add(tool);
        }

        public void SetActiveTool(ToolName name)
        {
            activeTool = tools.Find(tool => tool.name == name);
        }

        public void PointerDown()
        {
            if (activeTool == null)
            {
                return;
            }

            activeTool.Down();
        }

        public void PointerUp()
        {
            if (activeTool == null)
            {
                return;
            }

            activeTool.Up();
        }

        public void PointerClick()
        {
            if (activeTool == null)
            {
                return;
            }

            activeTool.Click();
        }

        public void PointerDrag()
        {
            if (activeTool == null)
            {
                return;
            }

            activeTool.Drag();
        }

        public Tool GetActiveTool()
        {
            return activeTool;
        }
    }
}
