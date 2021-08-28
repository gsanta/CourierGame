using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    // TODO: Create controller
    public class CourierListItem : MonoBehaviour
    {
        public TMP_Text courierNameText;
        [SerializeField]
        private Toggle followButton;
        [SerializeField]
        private Toggle playButton;

        private Biker courier;
        private CourierService courierService;
        private MainCamera mainCamera;

        public Biker Courier { 
            set {
                courier = value;
                courier.CurrentRoleChanged += HandleCurrentRoleChanged;
            }
        }

        public CourierService CourierService { set => courierService = value; }

        public MainCamera MainCamera { set => mainCamera = value; }

        public void HandleClickFollow()
        {
            bool isOn = !followButton.isOn;

            GetComponentInParent<BikerPanel>().ResetListItemsToggleButtons();
            ResetCourierStates();

            if (isOn)
            {
                SetCourierState(courier, false, true);
            }
        }

        public void HandleClickPlay()
        {
            bool isOn = !playButton.isOn;

            GetComponentInParent<BikerPanel>().ResetListItemsToggleButtons();

            ResetCourierStates();
            
            if (isOn)
            {
                SetCourierState(courier, true, true);
            }
        }

        private void HandleCurrentRoleChanged(object sender, EventArgs args)
        {
            var isPlay = courier.GetCurrentRole() == CurrentRole.PLAY;
            if (playButton.isOn != isPlay)
            {
                playButton.isOn = isPlay;
            }

            var isFollow = courier.GetCurrentRole() == CurrentRole.FOLLOW;
            if (followButton.isOn != isFollow)
            {
                followButton.isOn = isFollow;
            }
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
            followButton.isOn = false;
            playButton.isOn = false;
        }
    }
}
