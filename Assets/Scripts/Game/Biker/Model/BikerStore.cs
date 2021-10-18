using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bikers
{
    public class BikerStore : IClearableStore
    {
        private List<Biker> bikers = new List<Biker>();
        private MinimapBiker minimapBiker;
        private GameObject[] spawnPoints;
        private Biker bikerTemplate;
        private GameObject bikerContainer;

        public MinimapBiker GetMinimapBiker()
        {
            return minimapBiker;
        }

        public void SetMinimapBiker(MinimapBiker minimapBiker)
        {
            this.minimapBiker = minimapBiker;
        }

        public BikerStore()
        {
            Debug.Log("bikerStore");
        }

        public GameObject[] GetSpawnPoints() 
        {
            return spawnPoints;
        }

        public void SetSpawnPoints(GameObject[] spawnPoints)
        {
            this.spawnPoints = spawnPoints;
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

        public void Add(Biker biker)
        {
            bikers.Add(biker);
            TriggerCourierAdded(biker);
        }

        public List<Biker> GetAll()
        {
            return bikers;
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

        public void Clear()
        {
            bikers = new List<Biker>();
            minimapBiker = null;
            spawnPoints = null;
            bikerTemplate = null;
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




