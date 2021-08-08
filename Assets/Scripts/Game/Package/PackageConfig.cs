using UnityEngine;

public struct PackageConfig
{
    public readonly GameObject spawnPoint;

    public PackageConfig(GameObject spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }
}