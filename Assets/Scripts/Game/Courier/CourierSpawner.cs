using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class CourierSpawner : BaseSpawner<CourierConfig>
{
    private CourierStore courierStore;

    public CourierSpawner(CourierStore courierStore)
    {
        this.courierStore = courierStore;
    }

    override public void StartSpawning()
    {
        base.StartSpawning();
    }

    private void Spawn()
    {
        List<GameObject> usedSpawnPoints = new List<GameObject>(); 

        for (int i = 0; i < 3; i++)
        {

        }
    }

    private GameObject ChooseSpawnPoint(List<GameObject> usedSpawnPoints)
    {
        var freeSpawnPoints = courierStore.SpawnPoints.Where(spawnPoint => usedSpawnPoints.Contains(spawnPoint) == false).ToArray();
        int randomIndex = UnityEngine.Random.Range(0, freeSpawnPoints.Length);

        var spawnPoint = freeSpawnPoints[randomIndex];

        usedSpawnPoints.Add(spawnPoint);

        return spawnPoint;
    }
}
