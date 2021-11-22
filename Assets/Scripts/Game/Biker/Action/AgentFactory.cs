
using Actions;
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
        private List<GoapAction<Player>> bikerActions = new List<GoapAction<Player>>();
        private readonly ActionStore actionStore;
        private ActionFactory actionFactory;

        public AgentFactory(ActionStore actionStore, ActionFactory actionFactory)
        {
            idMap.Add("biker", 1);
            idMap.Add("pedestrian", 1);
            idMap.Add("enemy", 1);
            this.actionFactory = actionFactory;
            this.actionStore = actionStore;
        }

        public void AddBikerAction(GoapAction<Player> action)
        {
            bikerActions.Add(action);
        }   

        public GoapAgent<Enemy> CreateEnemyAgent(Enemy enemy)
        {
            string id = "enemy-" + idMap["enemy"];
            idMap["enemy"]++;


            var agent = new GoapAgent<Enemy>(id, enemy, new SimplePlanner<Enemy>());
            agent.SetActions(actionStore.GetEnemyActions());

            return agent;
        }

        public GoapAgent<Pedestrian> CreatePedestrianAgent(Pedestrian pedestrian)
        {
            string id = "pedestrian-" + idMap["pedestrian"];
            idMap["pedestrian"]++;

            var agent = new GoapAgent<Pedestrian>(id, pedestrian, new SimplePlanner<Pedestrian>());
            agent.SetActions(actionFactory.CreatePedestrianWalkAction(agent));
            agent.Active = false;

            return agent;
        }

        public GoapAgent<Player> CreateBikerAgent(Player biker)
        {
            List<GoapAction<Player>> actions = CloneBikerActions();
            string id = "biker-" + idMap["biker"];
            idMap["biker"]++;

            var agent = new GoapAgent<Player>(id, biker, new GoapPlanner<Player>());
            agent.SetActions(actions);

            return agent;
        }

        private List<GoapAction<Player>> CloneBikerActions()
        {
            return bikerActions.Select(action => action.Clone()).ToList();
        }
    }
}
