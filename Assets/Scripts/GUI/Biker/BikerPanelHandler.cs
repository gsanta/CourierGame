
using Bikers;
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
        private RoleService roleService;
        private PlayPanel bikerPanel;
        private BikerListItem.Factory bikerListItemFactory;
        private TurnManager turnManager;

        [Inject]
        public void Construct(PlayPanel bikerPanel, RoleService roleService, BikerListItem.Factory bikerListItemFactory, TurnManager turnManager)
        {
            this.bikerPanel = bikerPanel;
            this.roleService = roleService;
            this.bikerListItemFactory = bikerListItemFactory;
            this.turnManager = turnManager;
        }

        public void Play()
        {
            turnManager.Step();
        }

        private void Awake()
        {
            bikerPanel.SetBikerListItemInstantiator(this);
        }

        public IBikerListItem Instantiate(Biker biker)
        {
            BikerListItem bikerListItem = bikerListItemFactory.Create(bikerListTemplate);
            //bikerListTemplate.transform.parent
            bikerListItem.transform.SetParent(bikerListTemplate.transform.parent);
            bikerListItem.courierNameText.text = biker.GetName();
            bikerListItem.gameObject.SetActive(true);
            bikerListItem.SetBiker(biker);

            return bikerListItem;
        }
    }
}
