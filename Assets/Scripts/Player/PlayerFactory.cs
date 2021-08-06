using UnityEngine;

public class PlayerFactory : MonoBehaviour
{
    [SerializeField] private Player playerTemplate;
    [SerializeField] private GameObject minimapPlayerTemplate;
    
    private PackageStore packageStore;
    private DeliveryStore deliveryService;
    private TimelineController timelineController;
    private ITimeProvider timeProvider;
    private IWorldState worldState;
    private PlayerStore playerStore;
    private InputHandler inputHandler;

    public void SetDependencies(PackageStore packageStore, DeliveryStore deliveryService, TimelineController timelineController, ITimeProvider timeProvider, IWorldState worldState, PlayerStore playerStore, InputHandler inputHandler)
    {
        this.packageStore = packageStore;
        this.deliveryService = deliveryService;
        this.timelineController = timelineController;
        this.timeProvider = timeProvider;
        this.worldState = worldState;
        this.playerStore = playerStore;
        this.inputHandler = inputHandler;
    }

    public Player CreatePlayer(PlayerConfig config)
    {
        Player newPlayer = Instantiate(playerTemplate, playerTemplate.transform.parent);

        PlayerInputComponent playerInputComponent = new PlayerInputComponent(newPlayer, deliveryService, packageStore, inputHandler, playerStore);

        newPlayer.SetDependencies(playerInputComponent, playerStore, timeProvider, worldState);
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
