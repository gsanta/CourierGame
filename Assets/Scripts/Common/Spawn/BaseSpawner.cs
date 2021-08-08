using System;
using UnityEngine;

public abstract class BaseSpawner<T> : ISpawner<T>
{
    protected bool isSpawning = false;
    protected GameObject[] spawnPoints = { };

    public virtual void StartSpawning()
    {
        isSpawning = true;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    public void SetSpawnPoints(GameObject[] spawnPoints)
    {
        this.spawnPoints = spawnPoints;
    }

    protected void TriggerSpawn(SpawnEventArgs<T> e)
    {
        EventHandler<SpawnEventArgs<T>> handler = OnSpawn;
        if (handler != null)
        {
            handler(this, e);
        }
    }

    public event EventHandler<SpawnEventArgs<T>> OnSpawn;
}



