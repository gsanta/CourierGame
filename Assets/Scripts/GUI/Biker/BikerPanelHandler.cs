
using Bikers;
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
        private BikerPanel bikerPanel;
        private BikerListItem.Factory bikerListItemFactory;

        [Inject]
        public void Construct(BikerPanel bikerPanel, RoleService roleService, BikerListItem.Factory bikerListItemFactory)
        {
            this.bikerPanel = bikerPanel;
            this.roleService = roleService;
            this.bikerListItemFactory = bikerListItemFactory;
        }

        public void Play()
        {
            bikerPanel.Play();
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
            bikerListItem.RoleService = roleService;
            bikerListItem.SetBiker(biker);

            return bikerListItem;
        }
    }
}
