using UnityEngine;
using Zenject;

public class PlayerFactory: MonoBehaviour
{    
    private TimelineController timelineController;
    private Player.Factory instanceFactory;
    private PlayerStore playerStore;


    [Inject]
    public void Construct(Player.Factory instanceFactory, TimelineController timelineController, PlayerStore playerStore)
    {
        this.instanceFactory = instanceFactory;
        this.timelineController = timelineController;
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

        GameObject timeLineImage = timelineController.GetNextUnusedPlayerImage();
        timeLineImage.SetActive(true);
        newPlayer.TimelineImage = timeLineImage;

        playerStore.AddPlayer(newPlayer);

        return newPlayer;
    }
}
