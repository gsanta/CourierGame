

using System.Collections.Generic;

public class PlayerService
{
    private List<PlayerController> players = new List<PlayerController>();
    private PlayerController activePlayer;

    public void AddPlayer(PlayerController player)
    {
        players.Add(player);
    }

    public void ActivatePlayer(PlayerController player)
    {
        activePlayer = player;
    }
    
    public PlayerController GetPlayerById(int id)
    {
        return players.Find(player => player.id == id);
    }

    public PlayerController GetActivePlayer()
    {
        return activePlayer;
    }
}
