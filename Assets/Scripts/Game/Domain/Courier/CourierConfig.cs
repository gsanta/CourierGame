using AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public struct CourierConfig
{
    public readonly GameObject spawnPoint;
    public readonly SubGoal goal;
    public readonly string name;

    public CourierConfig(GameObject spawnPoint, SubGoal goal, string name)
    {
        this.spawnPoint = spawnPoint;
        this.goal = goal;
        this.name = name;
    }
}