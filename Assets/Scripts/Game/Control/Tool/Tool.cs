
namespace Controls
{
    public abstract class Tool
    {
        public readonly ToolName name;
        protected Tool(ToolName name)
        {
            this.name = name;
        }

        public virtual void Down() { }
        public virtual void Click() { }
        public virtual void RightClick() { }
        public virtual void Drag() { }
        public virtual void Up() { }
    }
}
