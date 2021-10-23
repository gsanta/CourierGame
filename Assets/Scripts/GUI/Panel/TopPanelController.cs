
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class TopPanelController : MonoBehaviour, IWidgetController
    {
        [Inject]
        public void Construct(PanelStore panelStore)
        {
            panelStore.AddWidgetController(this);
        }

        public List<GameObject> GetAllWidgets()
        {
            List<GameObject> widgets = new List<GameObject>();

            foreach (Transform item in transform)
            {
                widgets.Add(item.gameObject);
            }

            return widgets;
        }

        public T GetWidget<T>(Type type) where T : class
        {
            return GetComponentsInChildren(type, true)[0] as T;
        }

        public void Attack()
        {
            Debug.Log("Attack");
        }
    }
}
