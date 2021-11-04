
using Bikers;
using UI;
using UnityEngine;
using Zenject;

namespace Controls
{
    public class BikerPanelController : MonoBehaviour, IBikerListItemInstantiator
    {
        [SerializeField]
        private BikerListItem bikerListTemplate;
        private RoleService roleService;
        private BikerPanel bikerPanel;

        [Inject]
        public void Construct(BikerPanel bikerPanel, RoleService roleService)
        {
            this.bikerPanel = bikerPanel;
            this.roleService = roleService;
        }

        private void Awake()
        {
            bikerPanel.SetBikerListItemInstantiator(this);
        }

        public IBikerListItem Instantiate(Biker biker)
        {
            BikerListItem bikerListItem = Instantiate(bikerListTemplate, bikerListTemplate.transform.parent);
            bikerListItem.courierNameText.text = biker.GetName();
            bikerListItem.gameObject.SetActive(true);
            bikerListItem.RoleService = roleService;
            bikerListItem.SetBiker(biker);

            return bikerListItem;
        }
    }
}
