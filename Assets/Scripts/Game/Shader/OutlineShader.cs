using UnityEngine;

namespace Shaders
{
    public class OutlineShader
    {
        private float width = 1.2f;
        private float hiddenWidth = 0.1f;

        private GameObject gameObject;

        public OutlineShader(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public void Show()
        {
            var renderer = gameObject.transform.GetChild(0).GetComponent<Renderer>();

            renderer.material.SetFloat("_OutlineWidth", width);
        }

        public void Hide()
        {
            var renderer = gameObject.transform.GetChild(0).GetComponent<Renderer>();

            renderer.material.SetFloat("_OutlineWidth", hiddenWidth);
        }
    }
}
