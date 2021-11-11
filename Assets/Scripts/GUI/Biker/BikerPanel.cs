using Bikers;
using Scenes;
using Service;
using System;
using System.Collections.Generic;

namespace UI
{
    public class BikerPanel : IResetable
    {
        private List<IBikerListItem> bikerList = new List<IBikerListItem>();
        private IBikerListItemInstantiator bikerListItemInstantiator;

        private BikerService bikerService;
        private BikerStore bikerStore;

        private IBikerListItem prevActiveItem;

        public BikerPanel(BikerStore bikerStore, BikerService bikerService, EventService eventService)
        {
            this.bikerService = bikerService;
            this.bikerStore = bikerStore;

            bikerStore.OnBikerAdded += HandleBikerAdded;
            eventService.BikerCurrentRoleChanged += HandleBikerRoleChanged;

            bikerStore.GetAll().ForEach(biker => AddBiker(biker));
        }

        public void SetBikerListItemInstantiator(IBikerListItemInstantiator bikerListItemInstantiator)
        {
            this.bikerListItemInstantiator = bikerListItemInstantiator;
            ClearBikerItems();
            bikerStore.GetAll().ForEach(biker => AddBiker(biker));
        }

        private void HandleBikerAdded(object sender, CourierAddedEventArgs args)
        {
            AddBiker(args.Courier);
        }

        private void AddBiker(Biker biker)
        {
            if (bikerListItemInstantiator != null)
            {
                CreateBikerItem(biker);
            }
        }
    
        private void ClearBikerItems()
        {
            bikerList.ForEach(item => item.Destroy());
            bikerList.Clear();
        }

        private void CreateBikerItem(Biker biker)
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

        public void Reset()
        {
            ClearBikerItems();
        }
    }
}
