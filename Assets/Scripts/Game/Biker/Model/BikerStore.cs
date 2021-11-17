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
        private List<Biker> players = new List<Biker>();
        private MinimapBiker minimapBiker;
        private Biker bikerTemplate;
        private GameObject bikerContainer;
        private Biker activePlayer;

        private List<IObserver<BikerStoreInfo>> observers = new List<IObserver<BikerStoreInfo>>();

        public MinimapBiker GetMinimapBiker()
        {
            return minimapBiker;
        }

        public void SetMinimapBiker(MinimapBiker minimapBiker)
        {
            this.minimapBiker = minimapBiker;
        }

        public void SetBikerTemplate(Biker bikerTemplate)
        {
            this.bikerTemplate = bikerTemplate;
        }

        public Biker GetBikerTemplate()
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

        public void Add(Biker player)
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

        public void SetActivePlayer(Biker player)
        {
            this.activePlayer = player;

            var info = new BikerStoreInfo { type = BikerStoreInfo.Type.ACTIVE_PLAYER_CHANGED };
            foreach (var observer in observers.ToList())
                observer.OnNext(info);
        }

        public Biker GetActivePlayer()
        {
            return activePlayer;
        }

        public Biker GetFirstPlayer()
        {
            return players[0];
        }

        public Biker GetLastPlayer()
        {
            return players[players.Count - 1];
        }

        public Biker GetNextPlayer()
        {
            var nextPlayer = activePlayer == players[players.Count - 1] ? players[0] : players[players.IndexOf(activePlayer) + 1];
            return nextPlayer;
        }

        public List<Biker> GetAll()
        {
            return players;
        }

        public void Reset()
        {
            players = new List<Biker>();
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
        private readonly Biker player;

        internal PlayerAddedEventArgs(Biker player)
        {
            this.player = player;
        }
        public Biker Courier { get => player; }
    }
}




