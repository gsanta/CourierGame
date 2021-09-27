
using System.Collections.Generic;
using UnityEngine;

namespace Delivery
{
    public class PackageTargetPointStore : MonoBehaviour
    {
        [SerializeField]
        private GameObject container;

        private List<GameObject> spawnPoints = new List<GameObject>();

        private void Start()
        {
            foreach (Transform child in container.transform)
            {
                var obj = child.GetComponent<GameObject>();
                obj.SetActive(false);
                spawnPoints.Add(obj);
            }
        }

        public List<GameObject> GetAll()
        {
            return spawnPoints;
        }

        public int Size
        {
            get => spawnPoints.Count;
        }
    }
}
