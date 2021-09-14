using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class BikerStore : MonoBehaviour
    {
        private List<Biker> bikers = new List<Biker>();

        [SerializeField]
        private GameObject[] spawnPoints;
        [SerializeField]
        private Biker bikerTemplate;
        public GameObject[] SpawnPoints
        {
            get => spawnPoints;
        }

        public Biker BikerTemplate
        {
            get => bikerTemplate;
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

        private void Start()
        {
            foreach (var sp in spawnPoints)
            {
                sp.SetActive(false);
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




