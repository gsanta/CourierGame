using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace UI
{
    // TODO: Create controller
    public class CourierListItem : MonoBehaviour
    {
        public TMP_Text courierNameText;
        [SerializeField]
        private ToggleButton followButton;
        [SerializeField]
        private ToggleButton playButton;

        private Courier courier;
        private CourierService courierService;
        private MainCamera mainCamera;

        public Courier Courier { 
            set {
                courier = value;
                courier.CurrentRoleChanged += HandleCurrentRoleChanged;
            }
        }

        public CourierService CourierService { set => courierService = value; }

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
        }

        private void HandleCurrentRoleChanged(object sender, EventArgs args)
        {
            playButton.SetToggleState(courier.GetCurrentRole() == CurrentRole.PLAY);
            followButton.SetToggleState(courier.GetCurrentRole() == CurrentRole.FOLLOW);
        }

        private void ResetCourierStates()
        {
            ICourier courier = courierService.FindPlayOrFollowRole();
            if (courier != null)
            {
                courier.SetCurrentRole(CurrentRole.NONE);

                mainCamera.ResetPosition();
            }
        }

        private void SetCourierState(ICourier courier, bool isPlayer, bool isFollow)
        {
            // TODO maybe the role should come as a parameter and no need for the ifs
            if (isPlayer)
            {
                courier.SetCurrentRole(CurrentRole.PLAY);
            } else if (isFollow)
            {
                courier.SetCurrentRole(CurrentRole.FOLLOW);
            } else
            {
                courier.SetCurrentRole(CurrentRole.NONE);
            }
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
