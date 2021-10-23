
using Core;
using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    public class BuildingStore : IResetable
    {
        private GameObject buildingContainer;
        private Dictionary<string, GameObject> buildings = new Dictionary<string, GameObject>();

        public void SetBuildingContainer(GameObject buildingContainer)
        {
            this.buildingContainer = buildingContainer;

            foreach (Transform building in buildingContainer.transform)
            {
                buildings.Add(building.name, building.gameObject);
            }
        }

        public GameObject GetDoor(string buildingName)
        {
            if (buildingContainer != null)
            {
                GameObject building = buildings[buildingName];
                return building.transform.GetChild(1).gameObject;
            }
            return null;
        }

        public void Reset()
        {
            buildingContainer = null;
        }
    }
}
