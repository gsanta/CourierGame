using Model;
using Service;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    // TODO: Create controller
    public class BikerListItem : MonoBehaviour
    {
        public TMP_Text courierNameText;
        [SerializeField]
        private Toggle followButton;
        [SerializeField]
        private Toggle playButton;

        private Biker biker;
        private RoleService roleService;
        private bool isResetting = false;

        public Biker Biker { 
            set {
                biker = value;
                biker.CurrentRoleChanged += HandleCurrentRoleChanged;
            }

            get => biker;
        }

        public RoleService RoleService { set => roleService = value; }

        public void HandleClickFollow()
        {
            if (!isResetting)
            {
                SetCourierState(biker, false, followButton.isOn);
            }
        }

        public void HandleClickPlay()
        {
            if (!isResetting)
            {
                SetCourierState(biker, playButton.isOn, false);
            }
        }

        private void HandleCurrentRoleChanged(object sender, EventArgs args)
        {
            var isPlay = biker.GetCurrentRole() == CurrentRole.PLAY;
            //if (playButton.isOn != isPlay)
            //{
            //    playButton.isOn = isPlay;
            //}

            var isFollow = biker.GetCurrentRole() == CurrentRole.FOLLOW;
            //if (followButton.isOn != isFollow)
            //{
            //    followButton.isOn = isFollow;
            //}
        }

        private void SetCourierState(Biker biker, bool isPlayer, bool isFollow)
        {
            // TODO maybe the role should come as a parameter and no need for the ifs
            if (isPlayer)
            {
                roleService.SetCurrentRole(CurrentRole.PLAY, biker);
            } else if (isFollow)
            {
                roleService.SetCurrentRole(CurrentRole.FOLLOW, biker);
            } else
            {
                roleService.SetCurrentRole(CurrentRole.NONE, biker);
            }
        }

        public void ResetToggleButtons(bool isFollow, bool isPlay)
        {
            try
            {
                isResetting = true;

                if (followButton.isOn != isFollow)
                {
                    followButton.isOn = isFollow;
                }

                if (playButton.isOn != isPlay)
                {
                    playButton.isOn = isPlay;
                }
            } finally
            {
                isResetting = false;
            }
        }
    }
}
