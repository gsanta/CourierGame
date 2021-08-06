

using System.Collections.Generic;
using UnityEngine;

public class PlayerService : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private Player playerTemplate;
    [SerializeField] private int playerNum = 2;
    
    private DeliveryPackageController deliveryPackageController;
    private DeliveryService deliveryService;
    private InputHandler inputHandler;
    private TimelineController timelineController;
    private ITimeProvider timeProvider;
    private WorldState worldState;

    private List<Player> players = new List<Player>();
    private Player activePlayer;
    private HashSet<GameObject> occupiedSpawnPoints = new HashSet<GameObject>();

    public void SetDependencies(DeliveryPackageController deliveryPackageController, DeliveryService deliveryService, InputHandler inputHandler, TimelineController timelineController, ITimeProvider timeProvider, WorldState worldState)
    {
        this.deliveryPackageController = deliveryPackageController;
        this.deliveryService = deliveryService;
        this.inputHandler = inputHandler;
        this.timelineController = timelineController;
        this.timeProvider = timeProvider;
        this.worldState = worldState;
    }

    void Start()
    {
        CreatePlayers();
        activePlayer = players[0];
        inputHandler.OnKeyDown += OnKeyDown;
    }

    private void OnKeyDown(object sender, KeyDownEventArgs args)
    {
        SetNextPlayerAsActive();
    }

    public void SetNextPlayerAsActive()
    {
        if (activePlayer)
        {
            int activePlayerIndex = players.IndexOf(activePlayer);
            int nextPlayerIndex = (activePlayerIndex + 1) % players.Count;
            activePlayer = players[nextPlayerIndex];
        }
    }

    public void AddPlayer(Player player)
    {
        players.Add(player);
    }

    public void SetActivePlayer(Player player)
    {
        activePlayer = player;
    }

    public Player GetActivePlayer()
    {
        return activePlayer;
    }

    public void CreatePlayers()
    {
        for (int i = 0; i < playerNum; i++)
        {
            players.Add(CreatePlayer(GetRandomSpawnPoint(), "player-" + i));
        }

        occupiedSpawnPoints.Clear();
    }

    private GameObject GetRandomSpawnPoint()
    {
        if (occupiedSpawnPoints.Count == spawnPoints.Length)
        {
            return null;
        }

        while (true)
        {
            GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            if (!occupiedSpawnPoints.Contains(spawnPoint))
            {
                occupiedSpawnPoints.Add(spawnPoint);
                return spawnPoint;
            }
        }
    }

    public Player CreatePlayer(GameObject spawnPoint, string name)
    {
        Player newPlayer = Instantiate(playerTemplate, playerTemplate.transform.parent);
        newPlayer.SetDependencies(this, deliveryPackageController, deliveryService, timeProvider, worldState);
        newPlayer.transform.position = spawnPoint.transform.position;
        newPlayer.Name = name;
        newPlayer.gameObject.SetActive(true);

        GameObject timeLineImage = timelineController.GetNextUnusedPlayerImage();
        timeLineImage.SetActive(true);
        newPlayer.TimelineImage = timeLineImage;

        return newPlayer;
    }
}
