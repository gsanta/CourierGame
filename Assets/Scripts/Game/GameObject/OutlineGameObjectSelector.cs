using States;
using UnityEngine;

namespace GameObjects
{
    public class OutlineGameObjectSelector : IGameObjectSelector
    {
        private float width = 1.2f;
        private float hiddenWidth = 0.1f;

        private GameObject outlineGameObject;
        private ISelectableGameObject selectableGameObject;
        private SelectionStore selectionStore;

        public OutlineGameObjectSelector(SelectionStore selectionStore)
        {
            this.selectionStore = selectionStore;
        }

        public void SetOutlineGameObject(GameObject gameObject)
        {
            outlineGameObject = gameObject;
        }

        public void SetGameObject(ISelectableGameObject selectableGameObject)
        {
            this.selectableGameObject = selectableGameObject;
        }

        public void Deselect()
        {
            var renderer = outlineGameObject.GetComponent<Renderer>();

            renderer.material.SetFloat("_OutlineWidth", hiddenWidth);
            selectionStore.Remove(selectableGameObject);
        }

        public void Select()
        {
            var renderer = outlineGameObject.GetComponent<Renderer>();

            renderer.material.SetFloat("_OutlineWidth", width);
            selectionStore.Add(selectableGameObject);
        }
    }
}
