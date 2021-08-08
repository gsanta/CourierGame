
using UnityEngine;

public struct PlayerConfig
{
    public readonly string name;
    public readonly PlayerColor color;
    public readonly GameObject spawnPoint;
        
    public PlayerConfig(string name, PlayerColor color, GameObject spawnPoint)
    {
        this.name = name;
        this.color = color;
        this.spawnPoint = spawnPoint;
    }
}