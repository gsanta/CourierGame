
using Core;
using System.Collections.Generic;

namespace Scenes
{
    public class SceneChangeHandler
    {
        private List<IClearable> clearableStores = new List<IClearable>();
        private ISceneInitializer sceneInitializer;

        public void ClearPrevScene()
        {
            clearableStores.ForEach(store => store.Clear());
        }

        public void InitCurrentScene()
        {
            sceneInitializer.InitializeScene();
        }

        public void AddClearableStore(IClearable store)
        {
            clearableStores.Add(store);
        }

        public void SetSceneInitializer(ISceneInitializer sceneInitializer)
        {
            this.sceneInitializer = sceneInitializer;
        }
    }
}
