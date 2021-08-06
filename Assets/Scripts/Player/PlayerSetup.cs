
public class PlayerSetup
{
    private PlayerFactory playerFactory;
    private PlayerPool playerPool;
    private SpawnPointHandler spawnPointHandler;
    private PlayerStore playerStore;

    public PlayerSetup(PlayerPool playerPool, SpawnPointHandler spawnPointHandler, PlayerFactory playerFactory, PlayerStore playerStore)
    {
        this.playerPool = playerPool;
        this.spawnPointHandler = spawnPointHandler;
        this.playerFactory = playerFactory;
        this.playerStore = playerStore;
    }

    public void Setup()
    {
        playerPool.AddPlayerConfig(new PlayerConfig("Player-0", PlayerColor.Blue, spawnPointHandler.GetAndReserveRandomSpawnPoint()));
        playerPool.AddPlayerConfig(new PlayerConfig("Player-1", PlayerColor.Green, spawnPointHandler.GetAndReserveRandomSpawnPoint()));
        playerPool.AddPlayerConfig(new PlayerConfig("Player-2", PlayerColor.Yellow, spawnPointHandler.GetAndReserveRandomSpawnPoint()));

        spawnPointHandler.ReleaseAllSpawnPoints();

        foreach(PlayerConfig playerConfig in playerPool.GetAll())
        {
            Player player = playerFactory.CreatePlayer(playerConfig);
            playerStore.AddPlayer(player);
        }
    }
}
