
using Core;
using System.Collections.Generic;

namespace Scenes
{
    public class SceneChangeHandler
    {
        private List<IClearableStore> clearableStores = new List<IClearableStore>();
        private List<ISceneInitializer> sceneInitializers = new List<ISceneInitializer>();

        public void ClearPrevScene()
        {
            clearableStores.ForEach(store => store.Clear());
        }

        public void InitCurrentScene()
        {
            sceneInitializers.ForEach(initializer => initializer.InitializeScene());
        }

        public void AddClearableStore(IClearableStore store)
        {
            clearableStores.Add(store);
        }
    }
}
