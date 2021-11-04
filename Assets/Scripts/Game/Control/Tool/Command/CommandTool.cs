
using UnityEngine;

namespace Controls
{
    public class CommandTool : Tool
    {
        public CommandTool() : base(ToolName.COMMAND)
        {
        }

        public override void RightClick()
        {
            Debug.Log("rightclick");
        }
    }
}
