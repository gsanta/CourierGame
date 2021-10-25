
using UnityEngine;
using Worlds;

namespace UI
{
    public class CurfewButton
    {
        private readonly WorldStore worldStore;
        public Color color = Color.red;
        private IToolbarController toolbarController;

        public CurfewButton(WorldStore worldStore)
        {
            this.worldStore = worldStore;
        }

        public void SetToolbarController(IToolbarController toolbarController)
        {
            this.toolbarController = toolbarController;
        }

        public void UpdateColor()
        {
            if (worldStore.GetActiveWorld().Curfew)
            {
                color = Color.green;
            } else
            {
                color = Color.red;
            }

            toolbarController.UpdateButtonStates();
        }
    }
}
