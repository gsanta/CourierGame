using UnityEngine;
using Zenject;

public class PlayerFactory : MonoBehaviour
{
    [SerializeField] private Player playerTemplate;
    [SerializeField] private GameObject minimapPlayerTemplate;
    
    private TimelineController timelineController;
    private Player.Factory instanceFactory;

    [Inject]
    public void Construct(Player.Factory instanceFactory, TimelineController timelineController)
    {
        this.instanceFactory = instanceFactory;
        this.timelineController = timelineController;
    }

    public Player CreatePlayer(PlayerConfig config)
    {
        Player newPlayer = instanceFactory.Create(playerTemplate);

        newPlayer.transform.position = config.spawnPoint.transform.position;
        newPlayer.Name = config.name;
        newPlayer.gameObject.SetActive(true);

        GameObject newMinimapPlayer = Instantiate(minimapPlayerTemplate, playerTemplate.transform.parent);
        newMinimapPlayer.SetActive(true);
        newPlayer.minimapObject = newMinimapPlayer;

        GameObject timeLineImage = timelineController.GetNextUnusedPlayerImage();
        timeLineImage.SetActive(true);
        newPlayer.TimelineImage = timeLineImage;

        return newPlayer;
    }
}
