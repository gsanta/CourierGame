using AI;
using GamePlay;

namespace Bikers
{
    public class EnterBuildingPostAction : IGPostAction
    {
        private SceneManagerHolder sceneManagerHolder;

        public EnterBuildingPostAction(SceneManagerHolder sceneManagerHolder)
        {
            this.sceneManagerHolder = sceneManagerHolder;
        }

        public IGPostAction Clone()
        {
            return new EnterBuildingPostAction(sceneManagerHolder);
        }

        public void Execute()
        {
            sceneManagerHolder.D.EnterSubScene("Building");
        }
    }
}
