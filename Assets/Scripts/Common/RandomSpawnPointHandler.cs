
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnPointHandler : ISpawnPointHandler
{
    private PlayerStore playerStore;
    private HashSet<GameObject> reservedSpawnPoints = new HashSet<GameObject>();

    public RandomSpawnPointHandler(PlayerStore playerStore)
    {
        this.playerStore = playerStore;
    }

    public GameObject GetAndReserveSpawnPoint()
    {
        if (reservedSpawnPoints.Count == playerStore.SpawnPoints.Length)
        {
            return null;
        }

        while (true)
        {
            GameObject spawnPoint = playerStore.SpawnPoints[Random.Range(0, playerStore.SpawnPoints.Length)];
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
