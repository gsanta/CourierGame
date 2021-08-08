
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnPointHandler : ISpawnPointHandler
{
    private GameObject[] spawnPoints;
    private HashSet<GameObject> reservedSpawnPoints = new HashSet<GameObject>();

    public void SetSpawnPoints(GameObject[] spawnPoints)
    {
        this.spawnPoints = spawnPoints;
    }

    public GameObject GetAndReserveSpawnPoint()
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
