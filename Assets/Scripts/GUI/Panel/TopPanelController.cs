
using AI;
using Pedestrians;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Worlds;
using Zenject;

namespace GUI
{
    public class TopPanelController : MonoBehaviour, IWidgetController
    {
        private PedestrianStore pedestrianStore;
        private WorldStore worldStore;

        [Inject]
        public void Construct(PanelStore panelStore, PedestrianStore pedestrianStore, WorldStore worldStore)
        {
            panelStore.AddWidgetController(this);
            this.pedestrianStore = pedestrianStore;
            this.worldStore = worldStore;
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
    }
}
