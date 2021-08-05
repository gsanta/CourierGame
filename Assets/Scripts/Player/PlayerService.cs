

using System.Collections.Generic;
using UnityEngine;

public class PlayerService : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private Player playerTemplate;
    [SerializeField] private int playerNum = 2;
    [HideInInspector] public DeliveryPackageController deliveryPackageController;
    [HideInInspector] public DeliveryService deliveryService;
    [HideInInspector] public InputHandler inputHandler;
    [HideInInspector] public TimelineController timelineController;
    [HideInInspector] public ITimeProvider timeProvider;

    private List<Player> players = new List<Player>();
    private Player activePlayer;
    private HashSet<GameObject> occupiedSpawnPoints = new HashSet<GameObject>();

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
        newPlayer.timeProvider = timeProvider;
        newPlayer.playerService = this;
        newPlayer.packageService = deliveryPackageController;
        newPlayer.deliveryService = deliveryService;
        newPlayer.transform.position = spawnPoint.transform.position;
        newPlayer.Name = name;
        newPlayer.gameObject.SetActive(true);

        GameObject timeLineImage = timelineController.GetNextUnusedPlayerImage();
        timeLineImage.SetActive(true);
        newPlayer.TimelineImage = timeLineImage;

        return newPlayer;
    }
}
