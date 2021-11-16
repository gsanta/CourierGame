using Bikers;
using GamePlay;
using Scenes;
using Service;
using System;
using System.Collections.Generic;

namespace UI
{
    public class PlayPanel : IResetable
    {
        private List<IBikerListItem> itemList = new List<IBikerListItem>();
        private IBikerListItemInstantiator listItemInstantiator;

        private BikerStore bikerStore;
        private TurnManager turnManager;

        public PlayPanel(TurnManager turnManager, BikerStore bikerStore, EventService eventService)
        {
            this.bikerStore = bikerStore;
            this.turnManager = turnManager;
        }

        public void SetBikerListItemInstantiator(IBikerListItemInstantiator bikerListItemInstantiator)
        {
            this.listItemInstantiator = bikerListItemInstantiator;
            ClearItems();
            bikerStore.GetAll().ForEach(biker => CreateListItem(biker));
        }

        public void Play()
        {
            turnManager.Step();
        }
    
        private void ClearItems()
        {
            itemList.ForEach(item => item.Destroy());
            itemList.Clear();
        }

        private void CreateListItem(Biker biker)
        {
            IBikerListItem item = listItemInstantiator.Instantiate(biker);

            itemList.Add(item);
        }

        void PlayerSelected(Biker player)
        {
            if (turnManager.IsPlayerCommandTurn())
            {
                turnManager.ChangePlayer(player);
            }
        }

        public void Reset()
        {
            ClearItems();
        }
    }
}
