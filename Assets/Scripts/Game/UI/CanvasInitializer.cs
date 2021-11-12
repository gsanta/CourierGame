
namespace UI
{
    public class CanvasInitializer
    {
        private string[] panels;
        private CanvasStore canvasStore;

        public CanvasInitializer(CanvasStore canvasStore)
        {
            this.canvasStore = canvasStore;
        }

        public void SetInitialPanels(string[] panels)
        {
            this.panels = panels;
        }

        public void Initialize()
        {
            foreach (var panel in panels)
            {
                canvasStore.GetPanelByName(panel).SetActive(true);
            }
        }
    }
}
