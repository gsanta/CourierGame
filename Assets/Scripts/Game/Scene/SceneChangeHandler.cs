
using Core;
using System.Collections.Generic;

namespace Scenes
{
    public class SceneChangeHandler
    {
        private List<IResetable> resetableObjs = new List<IResetable>();
        private ISceneInitializer sceneInitializer;

        public void ClearPrevScene()
        {
            resetableObjs.ForEach(store => store.Reset());
        }

        public void InitCurrentScene()
        {
            sceneInitializer.InitializeScene();
        }

        public void AddResetable(IResetable obj)
        {
            resetableObjs.Add(obj);
        }

        public void SetSceneInitializer(ISceneInitializer sceneInitializer)
        {
            this.sceneInitializer = sceneInitializer;
        }
    }
}
