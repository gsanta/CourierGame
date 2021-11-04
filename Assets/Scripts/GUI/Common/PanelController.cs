﻿
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class PanelController : MonoBehaviour, IPanelController
    {
        [SerializeField]
        private GameObject panelContainer;

        [Inject]
        public void Construct(PanelStore panelStore)
        {
            panelStore.AddPanelController(this);
        }

        private void Start()
        {
            GetAllPanels().ForEach(panel => panel.SetActive(false));
            GetPanel<MenuPanel>(typeof(MenuPanel)).gameObject.SetActive(true);
        }

        public T GetPanel<T>(Type type) where T : class
        {
            return panelContainer.GetComponentsInChildren(type, true)[0] as T;
        }

        public void HidePanel(GameObject panel, float delay)
        {
            StartCoroutine(HideOrShowAfterDelay(panel, delay, false));
        }

        public void ShowPanel(GameObject panel, float delay)
        {
            StartCoroutine(HideOrShowAfterDelay(panel, delay, true));
        }

        private IEnumerator HideOrShowAfterDelay(GameObject panel, float delay, bool show)
        {
            yield return new WaitForSeconds(delay);
            panel.SetActive(show);
        }

        public List<GameObject> GetAllPanels()
        {
            List<GameObject> children = new List<GameObject>();

            foreach (Transform child in transform)
            {
                children.Add(child.gameObject);
            }

            return children;
        }
    }
}
