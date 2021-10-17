
using Core;
using System.Collections.Generic;

namespace Scenes
{
    public class SceneChangeHandler
    {
        private List<IClearableStore> clearableStores = new List<IClearableStore>();

        public void SceneChanged()
        {
            clearableStores.ForEach(store => store.Clear());
        }

        public void AddClearableStore(IClearableStore store)
        {
            clearableStores.Add(store);
        }
    }
}
