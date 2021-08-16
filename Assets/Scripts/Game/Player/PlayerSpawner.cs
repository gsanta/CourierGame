using UnityEngine;
using Zenject;

public class PlayerSpawner 
{
    private ISpawnPointHandler spawnPointHandler;

    private int Counter = 0;
    private PlayerColor[] colors = new PlayerColor[] { PlayerColor.Blue, PlayerColor.Green, PlayerColor.Yellow };

    [Inject]
    public void Construct(ISpawnPointHandler spawnPointHandler)
    {
        this.spawnPointHandler = spawnPointHandler;
    }

    public PlayerConfig Spawn()
    {
        PlayerConfig playerConfig = new PlayerConfig("Player-" + Counter, colors[Counter % colors.Length], spawnPointHandler.GetAndReserveSpawnPoint());
        Counter++;

        return playerConfig;
    }

     public void ReleaseAllSpawnPoints()
    {
        spawnPointHandler.ReleaseAllSpawnPoints();
    }
}
