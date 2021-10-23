
using Agents;
using AI;
using Pedestrians;
using System.Collections.Generic;
using System.Linq;

namespace Bikers
{
    public class AgentFactory
    {

        private Dictionary<string, int> idMap = new Dictionary<string, int>();
        private List<GoapAction<Biker>> bikerActions = new List<GoapAction<Biker>>();
        private List<GoapAction<Pedestrian>> pedestrianActions = new List<GoapAction<Pedestrian>>();
        private readonly ActionStore actionStore;
        private readonly PedestrianTargetStore pedestrianTargetStore;

        public AgentFactory(ActionStore actionStore, PedestrianTargetStore pedestrianTargetStore)
        {
            idMap.Add("biker", 1);
            idMap.Add("pedestrian", 1);
            this.actionStore = actionStore;
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
                
            actionStore.GetPedestrianActions().ForEach(action => actions.Add(action));

            Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();

            var agent = new GoapAgent<Pedestrian>(id, pedestrian, new PedestrianPlanner(actionStore));
            agent.SetActions(actions);

            return agent;
        }

        public GoapAgent<Biker> CreateBikerAgent(Biker biker)
        {
            List<GoapAction<Biker>> actions = CloneBikerActions();
            string id = "biker-" + idMap["biker"];
            idMap["biker"]++;

            var agent = new GoapAgent<Biker>(id, biker, new GoapPlanner<Biker>());
            agent.SetActions(actions);

            return agent;
        }

        private List<GoapAction<Biker>> CloneBikerActions()
        {
            return bikerActions.Select(action => action.Clone()).ToList();
        }
    }
}
