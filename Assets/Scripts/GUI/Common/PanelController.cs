
using System;
using System.Collections;
using UI;
using UnityEngine;
using Zenject;

namespace GUI
{
    public class PanelController : MonoBehaviour, IPanelController
    {
        [SerializeField]
        private GameObject panelContainer;

        [Inject]
        public void Construct(PanelManager panelManager)
        {
            panelManager.AddController(this);
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
    }
}
