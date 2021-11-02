
using GameObjects;
using System.Collections.Generic;
using UnityEngine;

namespace GUI
{
    public class ObjectClicker : MonoBehaviour
    {
        private List<GameObject> selection = new List<GameObject>();
        private string selectableTag = "Selectable";

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Camera.main)
                {
                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        var selection = hit.transform;
                        if (selection.CompareTag(selectableTag))
                        {

                            var selectableGameObject = selection.GetComponent<ISelectableGameObject>();// transform.GetChild(0).GetComponent<Renderer>();

                            selectableGameObject.GetGameObjectSelector().Deselect();
                        }
                    }
                }
            }
        }
    }
}
