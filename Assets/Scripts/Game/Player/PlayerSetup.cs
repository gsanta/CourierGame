
public class PlayerSetup
{
    private PlayerFactory playerFactory;
    private PlayerSpawner playerSpawner;
    private PlayerStore playerStore;

    public PlayerSetup(PlayerSpawner playerSpawner, PlayerFactory playerFactory, PlayerStore playerStore)
    {
        this.playerSpawner = playerSpawner;
        this.playerFactory = playerFactory;
        this.playerStore = playerStore;
    }

    public void Setup()
    {
        //playerPool.AddPlayerConfig(playerSpawner.Spawn());
        //playerPool.AddPlayerConfig(playerSpawner.Spawn());

        playerFactory.CreatePlayer(playerSpawner.Spawn());
    }
}
