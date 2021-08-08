using UnityEngine;


public interface ISpawnPointHandler
{

    public void SetSpawnPoints(GameObject[] gameObjects);
    public GameObject GetAndReserveSpawnPoint();
    public void ReleaseAllSpawnPoints();
}
