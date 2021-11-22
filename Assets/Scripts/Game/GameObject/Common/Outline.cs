using UnityEngine;

namespace GameObjects
{
    public class Outline : MonoBehaviour
    {
        private float width = 1.2f;
        private float hiddenWidth = 0.1f;

        [SerializeField]
        private GameObject target;

        private void Awake()
        {
            RemoveOutline();
        }

        public void SetOutline()
        {
            var renderer = target.GetComponent<Renderer>();

            renderer.material.SetFloat("_OutlineWidth", width);
        }

        public void RemoveOutline()
        {
            var renderer = target.GetComponent<Renderer>();

            renderer.material.SetFloat("_OutlineWidth", hiddenWidth);
        }
    }
}
