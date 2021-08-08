using System;

public class DefaultSpawner<T> : ISpawner<T>
{
    private bool isSpawning = false;

    public void StartSpawning()
    {
        isSpawning = true;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    private void TriggerSpawn(SpawnEventArgs<T> e)
    {
        EventHandler<SpawnEventArgs<T>> handler = OnSpawn;
        if (handler != null)
        {
            handler(this, e);
        }
    }

    public event EventHandler<SpawnEventArgs<T>> OnSpawn;
}



