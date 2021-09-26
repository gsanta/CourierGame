using Model;
using Service;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace UI
{
    public class BikerPanel : MonoBehaviour
    {
        [SerializeField]
        private BikerListItem courierListItemTemplate;
        private List<BikerListItem> courierList = new List<BikerListItem>();

        private BikerStore bikerStore;
        private BikerService bikerService;
        private IEventService eventService;
        private RoleService roleService;
        private BikerListItem prevActiveItem;

        [Inject]
        public void Construct(BikerStore bikerStore, BikerService bikerService, RoleService roleService, IEventService eventService)
        {
            this.bikerStore = bikerStore;
            this.bikerService = bikerService;
            this.roleService = roleService;
            this.eventService = eventService;
        }

        void Start()
        {
            bikerStore.OnBikerAdded += handleBikerAdded;
            eventService.BikerCurrentRoleChanged += HandleBikerRoleChanged;
        }

        private void handleBikerAdded(object sender, CourierAddedEventArgs args)
        {
            Biker courier = args.Courier;

            BikerListItem courierListItem = Instantiate(courierListItemTemplate, courierListItemTemplate.transform.parent);
            courierListItem.courierNameText.text = courier.GetName();
            courierListItem.gameObject.SetActive(true);
            courierListItem.RoleService = roleService;
            courierListItem.Biker = args.Courier;
            courierList.Add(courierListItem);
        }

        private void HandleBikerRoleChanged(object sender, EventArgs args)
        {
            if (prevActiveItem != null)
            {
                var prevBiker = prevActiveItem.Biker;
                prevActiveItem.ResetToggleButtons(prevBiker.GetCurrentRole() == CurrentRole.FOLLOW, prevBiker.GetCurrentRole() == CurrentRole.PLAY);
            }

            prevActiveItem = null;

            var activeBiker = bikerService.FindPlayOrFollowRole();

            if (activeBiker != null)
            {
                var listItem = courierList.Find(item => item.Biker == activeBiker);
                prevActiveItem = listItem;
            }
        }
    }
}
