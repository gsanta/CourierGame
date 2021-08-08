
public class PlayerSetup
{
    private PlayerFactory playerFactory;
    private PlayerPool playerPool;
    private PlayerSpawner playerSpawner;
    private PlayerStore playerStore;

    public PlayerSetup(PlayerPool playerPool, PlayerSpawner playerSpawner, PlayerFactory playerFactory, PlayerStore playerStore)
    {
        this.playerPool = playerPool;
        this.playerSpawner = playerSpawner;
        this.playerFactory = playerFactory;
        this.playerStore = playerStore;
    }

    public void Setup()
    {
        playerPool.AddPlayerConfig(playerSpawner.Spawn());
        playerPool.AddPlayerConfig(playerSpawner.Spawn());
        playerPool.AddPlayerConfig(playerSpawner.Spawn());

        playerSpawner.ReleaseAllSpawnPoints();

        foreach(PlayerConfig playerConfig in playerPool.GetAll())
        {
            Player player = playerFactory.CreatePlayer(playerConfig);
            playerStore.AddPlayer(player);
        }
    }
}
