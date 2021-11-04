
using GameObjects;
using UnityEngine;

namespace Controls
{
    public class ObjectClicker
    {
        private string selectableTag = "Selectable";

        public void Click()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                if (selection.CompareTag(selectableTag))
                {

                    var selectableGameObject = selection.GetComponent<ISelectableGameObject>();// transform.GetChild(0).GetComponent<Renderer>();

                    selectableGameObject.GetGameObjectSelector().Select();
                }
            }
        }        
    }
}
