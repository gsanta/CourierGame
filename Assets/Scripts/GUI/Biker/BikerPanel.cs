using Bikers;
using Service;
using System;
using System.Collections.Generic;

namespace UI
{
    public class BikerPanel
    {
        private List<IBikerListItem> bikerList = new List<IBikerListItem>();
        private IBikerListItemInstantiator bikerListItemInstantiator;

        private BikerService bikerService;

        private IBikerListItem prevActiveItem;

        public BikerPanel(BikerStore bikerStore, BikerService bikerService, EventService eventService)
        {
            this.bikerService = bikerService;

            bikerStore.OnBikerAdded += HandleBikerAdded;
            eventService.BikerCurrentRoleChanged += HandleBikerRoleChanged;

            bikerStore.GetAll().ForEach(biker => AddBiker(biker));
        }

        public void SetBikerListItemInstantiator(IBikerListItemInstantiator bikerListItemInstantiator)
        {
            this.bikerListItemInstantiator = bikerListItemInstantiator;
        }

        private void HandleBikerAdded(object sender, CourierAddedEventArgs args)
        {
            AddBiker(args.Courier);
        }

        private void AddBiker(Biker biker)
        {
            IBikerListItem bikerListItem = bikerListItemInstantiator.Instantiate(biker); 

            bikerList.Add(bikerListItem);
        }

        private void HandleBikerRoleChanged(object sender, EventArgs args)
        {
            if (prevActiveItem != null)
            {
                var prevBiker = prevActiveItem.GetBiker();
                prevActiveItem.ResetToggleButtons(prevBiker.GetCurrentRole() == CurrentRole.FOLLOW, prevBiker.GetCurrentRole() == CurrentRole.PLAY);
            }

            prevActiveItem = null;

            var activeBiker = bikerService.FindPlayOrFollowRole();

            if (activeBiker != null)
            {
                var listItem = bikerList.Find(item => item.GetBiker() == activeBiker);
                prevActiveItem = listItem;
            }
        }
    }
}
