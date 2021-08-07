
using UnityEngine;
using Zenject;

public class PackageSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnPoints;
    private ISpawnPointHandler spawnPointHandler;

    [Inject]
    public void Construct(ISpawnPointHandler spawnPointHandler)
    {
        this.spawnPointHandler = spawnPointHandler;
        this.spawnPointHandler.SetSpawnPoints(spawnPoints);
    }

    public PackageConfig Spawn()
    {
        PackageConfig config = new PackageConfig(spawnPointHandler.GetAndReserveSpawnPoint());

        return config;
    }
}
