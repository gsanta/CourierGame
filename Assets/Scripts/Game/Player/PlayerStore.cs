using System.Collections.Generic;
using UnityEngine;

public class PlayerStore
{
    private List<Player> players = new List<Player>();
    private Player activePlayer;

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
