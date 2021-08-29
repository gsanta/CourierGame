using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Controller
{
    public class PanelManager : MonoBehaviour
    {

        public GameObject deliveryPanel;
        public GameObject bikerPanel;

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
