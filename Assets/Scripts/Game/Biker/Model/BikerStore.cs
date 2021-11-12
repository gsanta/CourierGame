using Scenes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bikers
{
    public class BikerStoreInfo
    {
        public enum Type
        {
            ACTIVATED
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
            TriggerCourierAdded(player);
        }

        public void SetActivePlayer(Biker player)
        {
            this.activePlayer = player;

            var info = new BikerStoreInfo { type = BikerStoreInfo.Type.ACTIVATED };
            foreach (var observer in observers)
                observer.OnNext(info);
        }

        public Biker GetActivePlayer()
        {
            return activePlayer;
        }

        public List<Biker> GetAll()
        {
            return players;
        }

        public event EventHandler<CourierAddedEventArgs> OnBikerAdded;

        private void TriggerCourierAdded(Biker biker)
        {
            EventHandler<CourierAddedEventArgs> handler = OnBikerAdded;
            if (handler != null)
            {
                handler(this, new CourierAddedEventArgs(biker));
            }
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

            return null;
        }
    }

    public class CourierAddedEventArgs : EventArgs
    {
        private readonly Biker courier;

        internal CourierAddedEventArgs(Biker courier)
        {
            this.courier = courier;
        }
        public Biker Courier { get => courier; }
    }
}




