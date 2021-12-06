using Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameObjects
{
    public class PlayerStoreInfo
    {
        public enum Type
        {
            ACTIVE_PLAYER_CHANGED,
            PLAYER_ADDED
        }

        public Type type;
    }

    public class PlayerStore : IResetable, IObservable<PlayerStoreInfo>
    {
        private List<GameCharacter> players = new List<GameCharacter>();
        private MinimapBiker minimapBiker;
        private GameCharacter bikerTemplate;
        private GameObject bikerContainer;
        private GameCharacter activePlayer;

        private List<IObserver<PlayerStoreInfo>> observers = new List<IObserver<PlayerStoreInfo>>();

        public MinimapBiker GetMinimapBiker()
        {
            return minimapBiker;
        }

        public void SetMinimapBiker(MinimapBiker minimapBiker)
        {
            this.minimapBiker = minimapBiker;
        }

        public void SetBikerTemplate(GameCharacter bikerTemplate)
        {
            this.bikerTemplate = bikerTemplate;
        }

        public GameCharacter GetBikerTemplate()
        {
            return bikerTemplate;
        }

        public void SetBikerContainer(GameObject bikerContainer)
        {
            this.bikerContainer = bikerContainer;
        }

        public GameObject GetBikerContainer()
        {
            return bikerContainer;
        }

        public void Add(GameCharacter player)
        {
            if (activePlayer == null)
            {
                SetActivePlayer(player);
            }
            players.Add(player);

            var info = new PlayerStoreInfo { type = PlayerStoreInfo.Type.PLAYER_ADDED };
            foreach (var observer in observers.ToList())
                observer.OnNext(info);
        }

        public void SetActivePlayer(GameCharacter player)
        {
            this.activePlayer = player;

            var info = new PlayerStoreInfo { type = PlayerStoreInfo.Type.ACTIVE_PLAYER_CHANGED };
            foreach (var observer in observers.ToList())
                observer.OnNext(info);
        }

        public GameCharacter GetActivePlayer()
        {
            return activePlayer;
        }

        public GameCharacter GetFirstPlayer()
        {
            return players[0];
        }

        public GameCharacter GetLastPlayer()
        {
            return players[players.Count - 1];
        }

        public GameCharacter GetNextPlayer()
        {
            var nextPlayer = activePlayer == players[players.Count - 1] ? players[0] : players[players.IndexOf(activePlayer) + 1];
            return nextPlayer;
        }

        public List<GameCharacter> GetAll()
        {
            return players;
        }

        public void Reset()
        {
            players = new List<GameCharacter>();
            minimapBiker = null;
            bikerTemplate = null;
        }

        public IDisposable Subscribe(IObserver<PlayerStoreInfo> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }

            return new Unsubscriber<PlayerStoreInfo>(observers, observer);
        }
    }

    internal class Unsubscriber<BikerStoreInfo> : IDisposable
    {
        private List<IObserver<BikerStoreInfo>> observers;
        private IObserver<BikerStoreInfo> observer;

        internal Unsubscriber(List<IObserver<BikerStoreInfo>> observers, IObserver<BikerStoreInfo> observer)
        {
            this.observers = observers;
            this.observer = observer;
        }

        public void Dispose()
        {
            if (observers.Contains(observer))
                observers.Remove(observer);
        }
    }

    public class PlayerAddedEventArgs : EventArgs
    {
        private readonly GameCharacter player;

        internal PlayerAddedEventArgs(GameCharacter player)
        {
            this.player = player;
        }
        public GameCharacter Courier { get => player; }
    }
}




