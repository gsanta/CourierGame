using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ToggleButton : MonoBehaviour
    {
        private bool isOn;

        public void SetToggleState(bool isOn)
        {
            this.isOn = isOn;

            if (isOn)
            {
                GetComponent<Image>().color = Color.green;
            } else
            {
                GetComponent<Image>().color = Color.grey;
            }
        }

        public bool IsOn { get => isOn;  }
    }
}
