using Service.AI;
using UnityEngine;

public struct BikerConfig
{
    public readonly GameObject spawnPoint;
    public readonly SubGoal goal;
    public readonly string name;

    public BikerConfig(GameObject spawnPoint, SubGoal goal, string name)
    {
        this.spawnPoint = spawnPoint;
        this.goal = goal;
        this.name = name;
    }
}