using AI;
using System.Collections.Generic;
using UnityEngine;

namespace Pedestrians
{
    public class PedestrianGoalFactory
    {
        public void GoHome(Pedestrian pedestrian)
        {
            pedestrian.Agent.SetGoals(new List<Goal>() { new Goal(AIStateName.AT_HOME, true) }, true);
        }

        public void GoToPosition(Pedestrian pedestrian, Vector3 position)
        {
            pedestrian.Agent.SetGoals(new List<Goal>() { new Goal(AIStateName.DESTINATION_REACHED, true, position) }, true);
        }
    }
}
