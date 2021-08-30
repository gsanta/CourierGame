using AI;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BikerSpawner : BaseSpawner<CourierConfig>
{
    private BikerStore courierStore;
    private BikerFactory courierFactory;

    public BikerSpawner(BikerFactory courierFactory, BikerStore courierStore)
    {
        this.courierFactory = courierFactory;
        this.courierStore = courierStore;
    }

    override public void StartSpawning()
    {
        base.StartSpawning();
    }

    public void Spawn()
    {
        List<GameObject> usedSpawnPoints = new List<GameObject>(); 

        for (int i = 0; i < 1; i++)
        {
            GameObject spawnPoint = ChooseSpawnPoint(usedSpawnPoints);
            CourierConfig config = new CourierConfig(spawnPoint, new SubGoal("isPackageDropped", 1, true), $"Courier-{i}");
            Biker courier = courierFactory.Create(config);
            courierStore.Add(courier);
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
