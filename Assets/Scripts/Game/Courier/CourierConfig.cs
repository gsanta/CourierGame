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
    public SubGoal goal;

    public CourierConfig(GameObject spawnPoint, SubGoal goal)
    {
        this.spawnPoint = spawnPoint;
        this.goal = goal;
    }
}