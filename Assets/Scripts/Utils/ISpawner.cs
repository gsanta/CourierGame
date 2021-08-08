using System;

public interface ISpawner<T>
{
    public void StartSpawning();
    public void StopSpawning();
    public event EventHandler<SpawnEventArgs<T>> OnSpawn;
}

public class SpawnEventArgs<T> : EventArgs
{
    private T item;

    internal SpawnEventArgs(T item)
    {
        this.item = item;
    }
    public T Item { get => item; }
}