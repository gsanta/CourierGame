using AI;
using GamePlay;

namespace GameObjects
{
    public class EnterBuildingPostAction : IGPostAction
    {
        private SceneManager sceneManager;
        private SubsceneStore subsceneStore;

        public EnterBuildingPostAction(SceneManager sceneManager, SubsceneStore subsceneStore)
        {
            this.sceneManager = sceneManager;
            this.subsceneStore = subsceneStore;
        }

        public IGPostAction Clone()
        {
            return new EnterBuildingPostAction(sceneManager, subsceneStore);
        }

        public void Execute()
        {

            subsceneStore.Type = SubsceneType.BUILDING;
            sceneManager.EnterSubScene();
        }
    }
}
