using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace UI
{
    public class BikerPanel : MonoBehaviour
    {
        [SerializeField]
        private CourierListItem courierListItemTemplate;
        private List<CourierListItem> courierList = new List<CourierListItem>();

        private BikerStore bikerStore;
        private BikerService bikerService;
        private MainCamera mainCamera;

        private CourierListItem prevActiveItem;

        [Inject]
        public void Construct(BikerStore bikerStore, BikerService bikerService, MainCamera mainCamera)
        {
            this.bikerStore = bikerStore;
            this.bikerService = bikerService;
            this.mainCamera = mainCamera;
        }

        void Start()
        {
            bikerStore.OnBikerAdded += HandleCourierAdded;
            bikerService.CurrentRoleChanged += HandleBikerRoleChanged;
        }

        private void HandleCourierAdded(object sender, CourierAddedEventArgs args)
        {
            Biker courier = args.Courier;

            CourierListItem courierListItem = Instantiate(courierListItemTemplate, courierListItemTemplate.transform.parent);
            courierListItem.courierNameText.text = courier.GetName();
            courierListItem.gameObject.SetActive(true);
            courierListItem.CourierService = bikerService;
            courierListItem.Biker = args.Courier;
            courierListItem.MainCamera = mainCamera;
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
