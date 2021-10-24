
using AI;
using Pedestrians;
using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class TopPanelController : MonoBehaviour, IWidgetController
    {
        private PedestrianStore pedestrianStore;

        [Inject]
        public void Construct(PanelStore panelStore, PedestrianStore pedestrianStore)
        {
            panelStore.AddWidgetController(this);
            this.pedestrianStore = pedestrianStore;
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
            this.pedestrianStore.GetAll().ForEach(pedestrian =>
            {
                pedestrian.Agent.SetGoals(new List<SubGoal>() { new SubGoal("atHome", 3, true) }, true);
            });
            Debug.Log("Attack");
        }
    }
}
