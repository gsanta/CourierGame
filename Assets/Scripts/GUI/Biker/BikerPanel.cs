using Actions;
using AI;
using Bikers;
using Controls;
using Routes;
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
        private readonly RouteTool routeTool;
        private readonly ActionFactory actionFactory;
        private readonly RouteStore routeStore;
        private BikerStore bikerStore;
        private bool isPlayMode = false;

        private IBikerListItem prevActiveItem;

        public BikerPanel(BikerStore bikerStore, BikerService bikerService, EventService eventService, RouteTool routeTool, ActionFactory actionFactory, RouteStore routeStore)
        {
            this.bikerService = bikerService;
            this.routeTool = routeTool;
            this.actionFactory = actionFactory;
            this.routeStore = routeStore;
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

        public void Play()
        {
            if (!isPlayMode && bikerStore.GetActivePlayer() == bikerStore.GetLastPlayer())
            {
                isPlayMode = true;
            }

            if (!isPlayMode) 
            {
                routeStore.AddRoute(bikerStore.GetActivePlayer(), routeTool.GetPoints());
                bikerStore.SetNextPlayer();
                routeTool.Step();
            } else
            {

            }
            //var player = bikerStore.GetActivePlayer();
            //player.Agent.Active = true;
            //var points = routeTool.GetPoints();
            //points.RemoveAt(0);
            //player.Agent.SetActions(actionFactory.CreatePlayerWalkAction(player.Agent, points));
            //player.Agent.SetGoals(new Goal(AIStateName.WALK_FINISHED, false), false);
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
