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
            ResetCourierStates();

            if (isOn)
            {
                SetCourierState(courier, false, true);
            }
            
            followButton.SetToggleState(isOn);
        }

        public void HandleClickPlay()
        {
            bool isOn = !playButton.IsOn;

            GetComponentInParent<CourierPanel>().ResetListItemsToggleButtons();

            ResetCourierStates();
            
            if (isOn)
            {
                SetCourierState(courier, true, true);
            }

            playButton.SetToggleState(isOn);
        }

        private void ResetCourierStates()
        {
            ICourier activeCourier = courierStore.GetAll().Find(courier => courier.IsPlayer());
            ICourier followedCourier = courierStore.GetAll().Find(courier => courier.IsFollow());

            if (activeCourier != null)
            {
                activeCourier.SetPlayer(false);
            }

            if (followedCourier != null)
            {
                followedCourier.SetFollow(false);
            }

            mainCamera.ResetPosition();
        }

        private void SetCourierState(ICourier courier, bool isPlayer, bool isFollow)
        {
            courier.SetPlayer(isPlayer);
            courier.SetFollow(isFollow);
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
