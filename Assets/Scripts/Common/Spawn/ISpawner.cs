using System;
using UnityEngine;

public interface ISpawner<T>
{
    public void SetSpawnPoints(GameObject[] spawnPoints);
    public void StartSpawning();
    public void StopSpawning();
    public event EventHandler<SpawnEventArgs<T>> OnSpawn;
}

public class SpawnEventArgs<T> : EventArgs
{
    private T item;

    public SpawnEventArgs(T item)
    {
        this.item = item;
    }
    public T Item { get => item; }
}