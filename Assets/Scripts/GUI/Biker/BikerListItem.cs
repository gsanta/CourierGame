using Bikers;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class BikerListItem : MonoBehaviour, IBikerListItem, IObserver<BikerStoreInfo>
    {
        public TMP_Text courierNameText;
        [SerializeField]
        private Toggle followButton;
        [SerializeField]
        private Toggle playButton;

        private BikerStore bikerStore;

        private Biker player;
        private RoleService roleService;
        private bool isResetting = false;

        [Inject]
        public void Construct(BikerStore bikerStore)
        {
            this.bikerStore = bikerStore;
            bikerStore.Subscribe(this);
        }

        public void SetBiker(Biker biker)
        {
            this.player = biker;
            biker.CurrentRoleChanged += HandleCurrentRoleChanged;
            UpdateButtonState();
        }

        public Biker GetBiker()
        {
            return player;
        }

        public void SelectPlayer()
        {
            bikerStore.SetActivePlayer(player);
        }

        public RoleService RoleService { set => roleService = value; }

        public void HandleClickFollow()
        {
            if (!isResetting)
            {
                SetBikerState(player, false, followButton.isOn);
            }
        }

        public void HandleClickPlay()
        {
            if (!isResetting)
            {
                SetBikerState(player, playButton.isOn, false);
            }
        }

        private void HandleCurrentRoleChanged(object sender, EventArgs args)
        {
            var isPlay = player.GetCurrentRole() == CurrentRole.PLAY;
            //if (playButton.isOn != isPlay)
            //{
            //    playButton.isOn = isPlay;
            //}

            var isFollow = player.GetCurrentRole() == CurrentRole.FOLLOW;
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

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(BikerStoreInfo value)
        {
            UpdateButtonState();
        }

        private void UpdateButtonState()
        {
            if (player == bikerStore.GetActivePlayer())
            {
                var colors = GetComponent<Button>().colors;
                colors.normalColor = Color.green;
                GetComponent<Button>().colors = colors;
            }
            else
            {
                var colors = GetComponent<Button>().colors;
                colors.normalColor = Color.black;
                GetComponent<Button>().colors = colors;
            }
        }

        public class Factory : PlaceholderFactory<UnityEngine.Object, BikerListItem>
        {
        }
    }
}
