
using Agents;
using AI;
using Pedestrians;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bikers
{
    public class AgentFactory
    {

        private Dictionary<string, int> idMap = new Dictionary<string, int>();
        private List<GoapAction<Biker>> bikerActions = new List<GoapAction<Biker>>();
        private List<GoapAction<Pedestrian>> pedestrianActions = new List<GoapAction<Pedestrian>>();
        private readonly ActionProvider actionProvider;
        private readonly PedestrianTargetStore pedestrianTargetStore;

        public AgentFactory(ActionProvider actionProvider, PedestrianTargetStore pedestrianTargetStore)
        {
            idMap.Add("biker", 1);
            idMap.Add("pedestrian", 1);
            this.actionProvider = actionProvider;
            this.pedestrianTargetStore = pedestrianTargetStore;
        }

        public void AddBikerAction(GoapAction<Biker> action)
        {
            bikerActions.Add(action);
        }

        public void AddPedestrianAction(GoapAction<Pedestrian> action)
        {
            pedestrianActions.Add(action);
        }

        public GoapAgent<Pedestrian> CreatePedestrianAgent(Pedestrian pedestrian)
        {
            string id = "pedestrian-" + idMap["pedestrian"];
            idMap["pedestrian"]++;

            List<GoapAction<Pedestrian>> actions = new List<GoapAction<Pedestrian>>();
                
            actionProvider.GetWalkActions().ForEach(action => actions.Add(action));

            Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();

            var goalProvider = new GoalProvider(pedestrianTargetStore);

            var agent = new GoapAgent<Pedestrian>(id, pedestrian, pedestrian.navMeshAgent, pedestrian, goalProvider);
            agent.SetActions(actions);

            return agent;
        }

        private Dictionary<SubGoal, int> GetPedestrianGoals()
        {
            Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
            goals.Add(new SubGoal("isDestinationReached", 1, true), 3);

            return goals;
        }

        private List<GoapAction<Pedestrian>> ClonePedestrianActions()
        {
            return pedestrianActions.Select(action => action.Clone()).ToList();
        }

        public GoapAgent<Biker> CreateBikerAgent(Biker biker)
        {
            List<GoapAction<Biker>> actions = CloneBikerActions();
            string id = "biker-" + idMap["biker"];
            idMap["biker"]++;

            var goalProvider = new BikerGoalProvider();


            var agent = new GoapAgent<Biker>(id, biker, biker.navMeshAgent, biker, goalProvider);
            agent.SetActions(actions);

            return agent;
        }

        private List<GoapAction<Biker>> CloneBikerActions()
        {
            return bikerActions.Select(action => action.Clone()).ToList();
        }
    }
}
