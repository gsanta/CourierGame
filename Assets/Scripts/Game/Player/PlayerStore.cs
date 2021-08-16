using System.Collections.Generic;
using UnityEngine;

public class PlayerStore : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnPoints;
    [SerializeField]
    private Player playerTemplate;
    [SerializeField]
    private GameObject minimapPlayerTemplate;

    private List<Player> players = new List<Player>();
    private Player activePlayer;

    public GameObject[] SpawnPoints { get => spawnPoints; }
    public Player PlayerTemplate { get => playerTemplate; }
    public GameObject MinimapPlayerTemplate { get => minimapPlayerTemplate; }

    void Awake()
    {
        foreach(GameObject spawnPoint in spawnPoints)
        {
            spawnPoint.SetActive(false);
        }
    }

    public void SetNextPlayerAsActive()
    {
        if (activePlayer)
        {
            int activePlayerIndex = players.IndexOf(activePlayer);
            int nextPlayerIndex = (activePlayerIndex + 1) % players.Count;
            SetActivePlayer(players[nextPlayerIndex]);
        }
    }

    public void AddPlayer(Player player)
    {
        if (!activePlayer)
        {
            SetActivePlayer(player);
        }
        players.Add(player);
    }

    public void SetActivePlayer(Player player)
    {
        if (activePlayer)
        {
            activePlayer.DeactivatePlayer();
        }
        activePlayer = player;
        activePlayer.ActivatePlayer();
    }

    public bool IsActivePlayer(Player player)
    {
        return activePlayer == player;
    }

    public Player GetActivePlayer()
    {
        return activePlayer;
    }
}
