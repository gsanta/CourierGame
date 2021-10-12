

using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Stats;

namespace Delivery
{
    public class PackageSpawner : BaseSpawner<PackageConfig>
    {

        private Timer timer;
        private readonly PackageStore packageStore;
        private PackageSpawnPointStore packageSpawnPointStore;
        private ItemFactory<PackageConfig, Package> packageFactory;

        public PackageSpawner(Timer timer, PackageStore packageStore, PackageSpawnPointStore packageSpawnPointStore, PackageFactory packageFactory)
        {
            this.timer = timer;
            this.packageStore = packageStore;
            this.packageSpawnPointStore = packageSpawnPointStore;
            this.packageFactory = packageFactory;
            StartSpawning();
        }

        override public void StartSpawning()
        {
            base.StartSpawning();

            timer.SecondPassed += Spawn;
        }

        private void Spawn(object sender, EventArgs e)
        {
            int spawnCount = 5 - packageStore.GetPackagesOfStatus(DeliveryStatus.UNASSIGNED, DeliveryStatus.ASSIGNED).Count;

            if (packageStore.GetAll().Count > 0)
            {
                return;
            }

            List<GameObject> freeSpawnPoints = GetFreeSpawnPoints();


            while (spawnCount > 0)
            {

                int index = UnityEngine.Random.Range(0, freeSpawnPoints.Count);
                GameObject spawnPoint = freeSpawnPoints[index];

                freeSpawnPoints.RemoveAt(index);

                int price = UnityEngine.Random.Range(50, 100);


                PackageConfig config = new PackageConfig(spawnPoint, price);
                Package package = packageFactory.Create(config);

                packageStore.Add(package);

                spawnCount--;
            }
        }

        private List<GameObject> GetFreeSpawnPoints()
        {
            List<Package> occupiedPackages = packageStore.GetPackagesOfStatus(DeliveryStatus.UNASSIGNED, DeliveryStatus.ASSIGNED);
            List<GameObject> occupiedSpawnPoints = occupiedPackages.Select(package => package.SpawnPoint).ToList();

            List<GameObject> freeSpawnPoints = packageSpawnPointStore.GetAll().FindAll(spawnPoint => !occupiedSpawnPoints.Contains(spawnPoint));

            return new List<GameObject>(freeSpawnPoints);
        }
    }
}