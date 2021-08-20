using UnityEngine;
using Zenject;

public class PlayerFactory: MonoBehaviour
{    
    private Player.Factory instanceFactory;
    private PlayerStore playerStore;


    [Inject]
    public void Construct(Player.Factory instanceFactory, PlayerStore playerStore)
    {
        this.instanceFactory = instanceFactory;
        this.playerStore = playerStore;
    }

    public Player CreatePlayer(PlayerConfig config)
    {
        Player newPlayer = instanceFactory.Create(playerStore.PlayerTemplate);

        newPlayer.transform.position = config.spawnPoint.transform.position;
        newPlayer.Name = config.name;
        newPlayer.gameObject.SetActive(true);

        GameObject newMinimapPlayer = Instantiate(playerStore.MinimapPlayerTemplate, playerStore.PlayerTemplate.transform.parent);
        newMinimapPlayer.SetActive(true);
        newPlayer.minimapObject = newMinimapPlayer;

        playerStore.AddPlayer(newPlayer);

        return newPlayer;
    }
}
