
using Agents;
using AI;
using Enemies;
using Pedestrians;
using System.Collections.Generic;
using System.Linq;

namespace Bikers
{
    public class AgentFactory
    {

        private Dictionary<string, int> idMap = new Dictionary<string, int>();
        private List<GoapAction<Biker>> bikerActions = new List<GoapAction<Biker>>();
        private readonly ActionStore actionStore;

        public AgentFactory(ActionStore actionStore)
        {
            idMap.Add("biker", 1);
            idMap.Add("pedestrian", 1);
            idMap.Add("enemy", 1);
            this.actionStore = actionStore;
        }

        public void AddBikerAction(GoapAction<Biker> action)
        {
            bikerActions.Add(action);
        }   

        public GoapAgent<Enemy> CreateEnemyAgent(Enemy enemy)
        {
            string id = "enemy-" + idMap["enemy"];
            idMap["enemy"]++;

            List<GoapAction<Enemy>> actions = new List<GoapAction<Enemy>>();


            Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();

            var agent = new GoapAgent<Enemy>(id, enemy, null);
            agent.SetActions(actions);

            return agent;
        }

        public GoapAgent<Pedestrian> CreatePedestrianAgent(Pedestrian pedestrian)
        {
            string id = "pedestrian-" + idMap["pedestrian"];
            idMap["pedestrian"]++;

            var agent = new GoapAgent<Pedestrian>(id, pedestrian, new PedestrianPlanner());
            agent.SetActions(actionStore.GetPedestrianActions());

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
