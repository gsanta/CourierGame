using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Worlds;
using Zenject;

namespace Controls
{
    public class TopPanelController : MonoBehaviour, IToolbarController
    {
        [SerializeField]
        private Button button;
        private WorldStore worldStore;
        private CurfewButton curfewButton;

        [Inject]
        public void Construct(PanelStore panelStore, WorldStore worldStore, CurfewButton curfewButton)
        {
            panelStore.AddWidgetController(this);
            this.worldStore = worldStore;
            this.curfewButton = curfewButton;
            curfewButton.SetToolbarController(this);
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
            worldStore.GetActiveWorld().Curfew = !worldStore.GetActiveWorld().Curfew;
        }

        public void UpdateButtonStates()
        {
            button.GetComponent<Image>().color = curfewButton.color;
        }
    }
}
