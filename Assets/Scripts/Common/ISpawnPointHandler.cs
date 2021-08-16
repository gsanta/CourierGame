using UnityEngine;


public interface ISpawnPointHandler
{
    public GameObject GetAndReserveSpawnPoint();
    public void ReleaseAllSpawnPoints();
}
