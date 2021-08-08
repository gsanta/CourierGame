

using System;
using UnityEngine;

public class PackageSpawner : BaseSpawner<PackageConfig>
{

    private Timer timer;
    private PackageStore packageStore;

    public PackageSpawner(Timer timer, PackageStore packageStore)
    {
        this.timer = timer;
        this.packageStore = packageStore;
    }

    override public void StartSpawning()
    {
        base.StartSpawning();

        timer.OnSecondPassed += SpawnIfNeeded;
    }

    private void SpawnIfNeeded(object sender, EventArgs e)
    {
        if (packageStore.GetPackagesOfStatus(Package.DeliveryStatus.UNASSIGNED, Package.DeliveryStatus.ASSIGNED).Count == 0)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        GameObject spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        PackageConfig config = new PackageConfig(spawnPoint);

        TriggerSpawn(new SpawnEventArgs<PackageConfig>(config));
    }
}