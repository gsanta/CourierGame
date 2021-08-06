
using System.Collections.Generic;
using UnityEngine;

public class PlayerPool : MonoBehaviour
{
    // TODO: custom editor for this field
    public List<PlayerConfig> playerConfigs = new List<PlayerConfig>();

    private SpawnPointHandler spawnPointHandler;

    public void SetDependencies(SpawnPointHandler spawnPointHandler)
    {
        this.spawnPointHandler = spawnPointHandler;
    }

    public void AddPlayerConfig(PlayerConfig playerConfig)
    {
        playerConfigs.Add(playerConfig);
    }

    public List<PlayerConfig> GetAll()
    {
        return playerConfigs;
    }

    void Start()
    {
        playerConfigs = new List<PlayerConfig>();

        playerConfigs.Add(new PlayerConfig("Player-0", PlayerColor.Blue, spawnPointHandler.GetAndReserveRandomSpawnPoint()));
        playerConfigs.Add(new PlayerConfig("Player-1", PlayerColor.Green, spawnPointHandler.GetAndReserveRandomSpawnPoint()));
        playerConfigs.Add(new PlayerConfig("Player-2", PlayerColor.Yellow, spawnPointHandler.GetAndReserveRandomSpawnPoint()));

        spawnPointHandler.ReleaseAllSpawnPoints();
    }
}
