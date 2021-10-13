using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ToggleButton : MonoBehaviour
    {
        private bool isOn;

        public void SetToggleState(bool isOn)
        {
            this.isOn = isOn;

            GetComponent<Toggle>().isOn = isOn;
        }

        public bool IsOn { get => isOn;  }
    }
}
