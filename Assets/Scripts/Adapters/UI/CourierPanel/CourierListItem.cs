using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CourierListItem : MonoBehaviour
    {
        public TMP_Text courierNameText;
        [SerializeField]
        private ToggleButton followButton;
        [SerializeField]
        private ToggleButton playButton;

        private ICourier courier;
        private CourierStore courierStore;
        private MainCamera mainCamera;

        public ICourier Courier { set => courier = value;  }

        public CourierStore CourierStore { set => courierStore = value; }

        public MainCamera MainCamera { set => mainCamera = value; }

        public void HandleClickFollow()
        {
            bool isOn = !followButton.IsOn;

            GetComponentInParent<CourierPanel>().ResetListItemsToggleButtons();

            ICourier activeCourier = courierStore.GetAll().Find(courier => courier.IsActive());

            if (isOn)
            {
                if (activeCourier != null && activeCourier != courier)
                {
                    activeCourier.SetActive(false);
                }

                courier.SetActive(true);
            } else
            {
                courier.SetActive(false);
                mainCamera.ResetPosition();
            }

            followButton.SetToggleState(isOn);

            //courier.SetPlayer(true);
        }

        public void HandleClickPlay()
        {
            GetComponentInParent<CourierPanel>().ResetListItemsToggleButtons();

            ICourier activeCourier = courierStore.GetAll().Find(courier => courier.IsPlayer());
            if (activeCourier != null && activeCourier != courier)
            {
                activeCourier.SetPlayer(false);
            }

            courier.SetPlayer(true);
            playButton.SetToggleState(true);
        }

        public void ResetToggleButtons()
        {
            var toggleButtons = GetComponentsInChildren<ToggleButton>();

            foreach(var button in toggleButtons)
            {
                button.SetToggleState(false);
            }
        }
    }
}
