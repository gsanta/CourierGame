using GameObjects;
using System.Collections.Generic;

namespace States
{
    public class SelectionStore
    {
        List<ISelectableGameObject> selection = new List<ISelectableGameObject>();

        public void Add(ISelectableGameObject gameObject)
        {
            selection.Add(gameObject);
        }

        public void Remove(ISelectableGameObject gameObject)
        {
            selection.Remove(gameObject);
        }
        
        public void Clear()
        {
            selection.ForEach(selectable => selectable.GetGameObjectSelector().Deselect());
            selection.Clear();
        }
    }
}
