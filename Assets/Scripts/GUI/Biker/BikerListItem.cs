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
        private IDisposable subscription;
        private bool isActive = false;

        [Inject]
        public void Construct(BikerStore bikerStore)
        {
            subscription = bikerStore.Subscribe(this);
        }

        public bool IsActive {
            get => isActive;
            set {
                isActive = value;
                UpdateButtonState();
            } 
        }

        public Button GetButton()
        {
            return GetComponent<Button>();
        }

        public void Destroy()
        {
            subscription.Dispose();
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
            if (value.type == BikerStoreInfo.Type.ACTIVE_PLAYER_CHANGED)
            {
                UpdateButtonState();
            }
        }

        private void UpdateButtonState()
        {
            if (isActive)
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
