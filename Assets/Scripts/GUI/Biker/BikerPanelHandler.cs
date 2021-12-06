
using GameObjects;
using GamePlay;
using UI;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class BikerPanelHandler : MonoBehaviour, IBikerListItemInstantiator
    {
        [SerializeField]
        private BikerListItem bikerListTemplate;
        private BikerListItem.Factory bikerListItemFactory;
        private TurnManager turnManager;

        [Inject]
        public void Construct(BikerListItem.Factory bikerListItemFactory, TurnManager turnManager, GameObjectStore gameObjectStore)
        {
            this.bikerListItemFactory = bikerListItemFactory;
            this.turnManager = turnManager;

            gameObjectStore.BikerListItemInstantiator = this;
        }

        public void Play()
        {
            turnManager.Step();
        }

        public IBikerListItem Instantiate(string text)
        {
            BikerListItem bikerListItem = bikerListItemFactory.Create(bikerListTemplate);
            //bikerListTemplate.transform.parent
            bikerListItem.transform.SetParent(bikerListTemplate.transform.parent);
            bikerListItem.courierNameText.text = text;
            bikerListItem.gameObject.SetActive(true);

            return bikerListItem;
        }
    }
}
