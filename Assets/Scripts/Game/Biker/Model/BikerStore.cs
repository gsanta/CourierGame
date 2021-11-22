using Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bikers
{
    public class BikerStoreInfo
    {
        public enum Type
        {
            ACTIVE_PLAYER_CHANGED,
            PLAYER_ADDED
        }

        public Type type;
    }

    public class BikerStore : IResetable, IObservable<BikerStoreInfo>
    {
        private List<Player> players = new List<Player>();
        private MinimapBiker minimapBiker;
        private Player bikerTemplate;
        private GameObject bikerContainer;
        private Player activePlayer;

        private List<IObserver<BikerStoreInfo>> observers = new List<IObserver<BikerStoreInfo>>();

        public MinimapBiker GetMinimapBiker()
        {
            return minimapBiker;
        }

        public void SetMinimapBiker(MinimapBiker minimapBiker)
        {
            this.minimapBiker = minimapBiker;
        }

        public void SetBikerTemplate(Player bikerTemplate)
        {
            this.bikerTemplate = bikerTemplate;
        }

        public Player GetBikerTemplate()
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

        public void Add(Player player)
        {
            if (activePlayer == null)
            {
                SetActivePlayer(player);
            }
            players.Add(player);

            var info = new BikerStoreInfo { type = BikerStoreInfo.Type.PLAYER_ADDED };
            foreach (var observer in observers.ToList())
                observer.OnNext(info);
        }

        public void SetActivePlayer(Player player)
        {
            this.activePlayer = player;

            var info = new BikerStoreInfo { type = BikerStoreInfo.Type.ACTIVE_PLAYER_CHANGED };
            foreach (var observer in observers.ToList())
                observer.OnNext(info);
        }

        public Player GetActivePlayer()
        {
            return activePlayer;
        }

        public Player GetFirstPlayer()
        {
            return players[0];
        }

        public Player GetLastPlayer()
        {
            return players[players.Count - 1];
        }

        public Player GetNextPlayer()
        {
            var nextPlayer = activePlayer == players[players.Count - 1] ? players[0] : players[players.IndexOf(activePlayer) + 1];
            return nextPlayer;
        }

        public List<Player> GetAll()
        {
            return players;
        }

        public void Reset()
        {
            players = new List<Player>();
            minimapBiker = null;
            bikerTemplate = null;
        }

        public IDisposable Subscribe(IObserver<BikerStoreInfo> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }

            return new Unsubscriber<BikerStoreInfo>(observers, observer);
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
        private readonly Player player;

        internal PlayerAddedEventArgs(Player player)
        {
            this.player = player;
        }
        public Player Courier { get => player; }
    }
}




