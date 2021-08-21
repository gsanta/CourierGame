using AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CourierSpawner : BaseSpawner<CourierConfig>
{
    private CourierStore courierStore;
    private ItemFactory<CourierConfig, CourierAgent> courierFactory;

    public CourierSpawner(CourierStore courierStore, ItemFactory<CourierConfig, CourierAgent> courierFactory)
    {
        this.courierStore = courierStore;
        this.courierFactory = courierFactory;
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
            CourierAgent courier = courierFactory.Create(config);
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
