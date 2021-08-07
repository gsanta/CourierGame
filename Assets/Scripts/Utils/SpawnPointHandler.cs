
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointHandler
{
    private GameObject[] spawnPoints;
    private HashSet<GameObject> reservedSpawnPoints = new HashSet<GameObject>();

    public SpawnPointHandler(GameObject[] spawnPoints)
    {
        this.spawnPoints = spawnPoints;
    }

    public GameObject GetAndReserveRandomSpawnPoint()
    {
        if (reservedSpawnPoints.Count == spawnPoints.Length)
        {
            return null;
        }

        while (true)
        {
            GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            if (!reservedSpawnPoints.Contains(spawnPoint))
            {
                reservedSpawnPoints.Add(spawnPoint);
                return spawnPoint;
            }
        }
    }

    public void ReleaseAllSpawnPoints()
    {
        reservedSpawnPoints.Clear();
    }
}
