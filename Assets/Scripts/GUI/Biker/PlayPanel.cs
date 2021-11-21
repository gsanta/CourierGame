using Bikers;
using GamePlay;
using Scenes;
using System;
using System.Collections.Generic;

namespace UI
{
    public class PlayPanel : IResetable, IObserver<BikerStoreInfo>
    {
        private List<IBikerListItem> itemList = new List<IBikerListItem>();

        private BikerStore playerStore;
        private TurnManager turnManager;
        private TurnHelper turnHelper;
        private GameObjectStore gameObjectStore;

        public PlayPanel(TurnManager turnManager, TurnHelper turnHelper, BikerStore playerStore, GameObjectStore gameObjectStore)
        {
            this.playerStore = playerStore;
            this.turnManager = turnManager;
            this.turnHelper = turnHelper;
            this.gameObjectStore = gameObjectStore;

            turnManager.TurnChanged += HandleTurnChanged;

            playerStore.Subscribe(this);
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

        private void CreateListItem(string text, bool isActive)
        {
            if (gameObjectStore.BikerListItemInstantiator != null)
            {
                IBikerListItem item = gameObjectStore.BikerListItemInstantiator.Instantiate(text);
                item.IsActive = isActive;
                itemList.Add(item);
            }
        }

        public void Reset()
        {
            ClearItems();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(BikerStoreInfo value)
        {
            if (value.type == BikerStoreInfo.Type.PLAYER_ADDED || value.type == BikerStoreInfo.Type.ACTIVE_PLAYER_CHANGED)
            {
                UpdateListItems();
            }
        }

        private void UpdateListItems()
        {
            ClearItems();
            playerStore.GetAll().ForEach(player => CreateListItem(player.GetName(), playerStore.GetActivePlayer() == player && (turnManager.IsPlayerCommandTurn() || turnManager.IsPlayerPlayTurn())));
            CreateListItem("Civilians", turnManager.IsPedestrianTurn());
            CreateListItem("Enemies", turnManager.IsEnemyTurn());
        }

        private void HandleTurnChanged(object sender, EventArgs args)
        {
            UpdateListItems();
        }
    }
}
