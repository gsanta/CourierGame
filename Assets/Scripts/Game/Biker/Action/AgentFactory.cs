
using AI;
using Pedestrians;
using System.Collections.Generic;
using System.Linq;

namespace Bikers
{
    public class AgentFactory
    {

        private Dictionary<string, int> idMap = new Dictionary<string, int>();
        private List<GoapAction<Biker>> actions = new List<GoapAction<Biker>>();
        private List<GoapAction<Pedestrian>> pedestrianActions = new List<GoapAction<Pedestrian>>();

        public AgentFactory()
        {
            idMap.Add("biker", 1);
        }

        public void AddBikerAction(GoapAction<Biker> action)
        {
            actions.Add(action);
        }

        public void AddPedestrianAction(GoapAction<Pedestrian> action)
        {
            pedestrianActions.Add(action);
        }

        public GoapAgent<Pedestrian> CreatePedestrianAgent(Pedestrian pedestrian)
        {
            //List<GoapAction<Biker>> actions = CloneBikerActions();
            //string id = "biker-" + idMap["biker"];
            //idMap["biker"]++;

            //var agent = new GoapAgent<Pedestrian>(id, pedestrian, pedestrian.navMeshAgent, pedestrian, GetBikerGoals());
            //agent.SetActions(actions);

            //return agent;
            return null;
        }

        private Dictionary<SubGoal, int> GetPedestrianGoals()
        {
            Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
            goals.Add(new SubGoal("isPackageDropped", 1, true), 3);

            return goals;
        }

        public GoapAgent<Biker> CreateBikerAgent(Biker biker)
        {
            List<GoapAction<Biker>> actions = CloneBikerActions();
            string id = "biker-" + idMap["biker"];
            idMap["biker"]++;

            var agent = new GoapAgent<Biker>(id, biker, biker.navMeshAgent, biker, GetBikerGoals());
            agent.SetActions(actions);

            return agent;
        }

        private List<GoapAction<Biker>> CloneBikerActions()
        {
            return actions.Select(action => action.Clone()).ToList();
        }

        private Dictionary<SubGoal, int> GetBikerGoals()
        {
            Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
            goals.Add(new SubGoal("isPackageDropped", 1, true), 3);

            return goals;
        }
    }
}
