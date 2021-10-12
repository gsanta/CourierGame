using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bikers
{
    public class BikerStore
    {
        private List<Biker> bikers = new List<Biker>();

        private GameObject[] spawnPoints;
        private Biker bikerTemplate;

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




