using UnityEngine;

public class PlayerSpawner : MonoBehaviour 
{
    [SerializeField]
    private GameObject[] spawnPoints;
    private SpawnPointHandler spawnPointHandler;
    private int Counter = 0;
    private PlayerColor[] colors = new PlayerColor[] { PlayerColor.Blue, PlayerColor.Green, PlayerColor.Yellow };

    public PlayerConfig Spawn()
    {
        if (spawnPointHandler == null) { 
            spawnPointHandler = new SpawnPointHandler(spawnPoints);
        }

        PlayerConfig playerConfig = new PlayerConfig("Player-" + Counter, colors[Counter % colors.Length], spawnPointHandler.GetAndReserveRandomSpawnPoint());
        Counter++;

        return playerConfig;
    }

    public void ReleaseAllSpawnPoints()
    {
        spawnPointHandler.ReleaseAllSpawnPoints();
    }
}
