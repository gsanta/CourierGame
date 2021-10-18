using Bikers;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BikerListItem : MonoBehaviour, IBikerListItem
    {
        public TMP_Text courierNameText;
        [SerializeField]
        private Toggle followButton;
        [SerializeField]
        private Toggle playButton;

        private Biker biker;
        private RoleService roleService;
        private bool isResetting = false;

        public void SetBiker(Biker biker)
        {
            this.biker = biker;
            biker.CurrentRoleChanged += HandleCurrentRoleChanged;
        }

        public Biker GetBiker()
        {
            return biker;
        }

        public RoleService RoleService { set => roleService = value; }

        public void HandleClickFollow()
        {
            if (!isResetting)
            {
                SetBikerState(biker, false, followButton.isOn);
            }
        }

        public void HandleClickPlay()
        {
            if (!isResetting)
            {
                SetBikerState(biker, playButton.isOn, false);
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

        private void SetBikerState(Biker biker, bool isPlayer, bool isFollow)
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
