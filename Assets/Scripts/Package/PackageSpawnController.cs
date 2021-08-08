
using UnityEngine;
using Zenject;

public class PackageSpawnController : MonoBehaviour, ISpawnPointsProvider
{
    [SerializeField]
    private GameObject[] spawnPoints;
    private ISpawner<PackageConfig> spawner;
    private ItemFactory<PackageConfig, Package> itemFactory;
    private PackageStore packageStore;

    [Inject]
    public void Construct(ISpawner<PackageConfig> spawner, ItemFactory<PackageConfig, Package> itemFactory, PackageStore packageStore)
    {
        this.spawner = spawner;
        this.itemFactory = itemFactory;
        this.packageStore = packageStore;
    }

    void Start()
    {
        spawner.SetSpawnPoints(spawnPoints);
        spawner.StartSpawning();
        spawner.OnSpawn += OnSpawn;
    }

    public GameObject[] GetSpawnPoints()
    {
        return spawnPoints;
    }

    private void OnSpawn(object sender, SpawnEventArgs<PackageConfig> e)
    {
        Package package = itemFactory.Create(e.Item);
        packageStore.Add(package);
    }
}
