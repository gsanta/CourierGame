using UnityEngine;

public class SpawnController<T, U>
{
    private ISpawner<T> spawner;
    private ItemFactory<T, U> itemFactory;

    public SpawnController(ISpawner<T> spawner, ItemFactory<T, U> itemFactory)
    {

    }
}
