﻿

using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PackageSpawner : BaseSpawner<PackageConfig>
{

    private Timer timer;
    private PackageStore packageStore;
    private ItemFactory<PackageConfig, Package> packageFactory;

    public PackageSpawner(Timer timer, PackageStore packageStore, ItemFactory<PackageConfig, Package> packageFactory)
    {
        this.timer = timer;
        this.packageStore = packageStore;
        this.packageFactory = packageFactory;
        StartSpawning();
    }

    override public void StartSpawning()
    {
        base.StartSpawning();

        timer.OnSecondPassed += Spawn;
    }

    private void Spawn(object sender, EventArgs e)
    {
        int spawnCount = 5 - packageStore.GetPackagesOfStatus(Package.DeliveryStatus.UNASSIGNED, Package.DeliveryStatus.ASSIGNED).Count;

        if (spawnCount <= 0)
        {
            return;
        }

        List<GameObject> freeSpawnPoints = GetFreeSpawnPoints();

        while (spawnCount > 0)
        {

            int index = UnityEngine.Random.Range(0, freeSpawnPoints.Count);
            GameObject spawnPoint = freeSpawnPoints[index];
            
            freeSpawnPoints.RemoveAt(index);

            PackageConfig config = new PackageConfig(spawnPoint);
            Package package = packageFactory.Create(config);

            packageStore.Add(package);

            spawnCount--;
        }
    }

    private List<GameObject> GetFreeSpawnPoints()
    {
        List<Package> occupiedPackages = packageStore.GetPackagesOfStatus(Package.DeliveryStatus.UNASSIGNED, Package.DeliveryStatus.ASSIGNED);
        List<GameObject> occupiedSpawnPoints = occupiedPackages.Select(package => package.SpawnPoint).ToList();

        GameObject[] freeSpawnPoints = Array.FindAll(packageStore.PackageSpawnPoints, spawnPoint => !occupiedSpawnPoints.Contains(spawnPoint));

        return new List<GameObject>(freeSpawnPoints);
    }
}