using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Delivery
{
    public class PackageSpawnPointStore : IClearableStore
    {
        private List<GameObject> spawnPoints = new List<GameObject>();
        
        public List<GameObject> GetAll()
        {
            return spawnPoints;
        }

        public void Add(GameObject spawnPoint)
        {
            spawnPoints.Add(spawnPoint);
        }

        public void Clear()
        {
            spawnPoints = new List<GameObject>();
        }

        public int Size
        {
            get => spawnPoints.Count;
        }
    }
}
