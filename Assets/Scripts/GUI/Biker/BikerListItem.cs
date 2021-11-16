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
        [SerializeField]
        public TMP_Text courierNameText;
        private BikerStore bikerStore;
        private Biker player;

        [Inject]
        public void Construct(BikerStore bikerStore)
        {
            this.bikerStore = bikerStore;
            bikerStore.Subscribe(this);
        }

        public Button GetButton()
        {
            return GetComponent<Button>();
        }

        public void SetBiker(Biker biker)
        {
            this.player = biker;
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
