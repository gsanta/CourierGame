using AI;
using GamePlay;
using Worlds;

namespace GameObjects
{
    public class ExitBuildingPostAction : IGPostAction
    {
        private SceneManager sceneManager;
        private WorldStore worldStore;

        public ExitBuildingPostAction(SceneManager sceneManager, WorldStore worldStore)
        {
            this.sceneManager = sceneManager;
            this.worldStore = worldStore;
        }

        public IGPostAction Clone()
        {
            return new ExitBuildingPostAction(sceneManager, worldStore);
        }

        public void Execute()
        {
            sceneManager.ExitSubScene();
        }
    }
}
