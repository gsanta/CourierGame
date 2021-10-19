using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Pedestrians
{
    public class PedestrianStore : IClearable
    {

        private Pedestrian pedestrianTemplate;
        private GameObject pedestrianContainer;

        private List<Pedestrian> pedestrians = new List<Pedestrian>();

        public void SetPedestrianTemplate(Pedestrian pedestrian)
        {
            pedestrianTemplate = pedestrian;
        }

        public Pedestrian GetPedestrianTemplate()
        {
            return pedestrianTemplate;
        }

        public void SetPedestrianContainer(GameObject pedestrianContainer)
        {
            this.pedestrianContainer = pedestrianContainer;
        }

        public GameObject GetPedestrianContainer()
        {
            return pedestrianContainer;
        }

        public void Add(Pedestrian pedestrian)
        {
            pedestrians.Add(pedestrian);
        }

        public List<Pedestrian> GetAll()
        {
            return pedestrians;
        }

        public void Clear()
        {
            pedestrianTemplate = null;
            pedestrianContainer = null;
            pedestrians = new List<Pedestrian>();
        }
    }
}
