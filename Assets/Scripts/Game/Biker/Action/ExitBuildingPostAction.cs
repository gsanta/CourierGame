using AI;
using GamePlay;
using Worlds;

namespace GameObjects
{
    public class ExitBuildingPostAction : IGPostAction
    {
        private SceneManagerHolder sceneManager;
        private WorldStore worldStore;

        public ExitBuildingPostAction(SceneManagerHolder sceneManager, WorldStore worldStore)
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
            sceneManager.D.ExitSubScene(worldStore.CurrentMap);
        }
    }
}
