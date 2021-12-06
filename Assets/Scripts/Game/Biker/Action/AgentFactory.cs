
using Actions;
using Agents;
using AI;
using Enemies;
using Pedestrians;
using System.Collections.Generic;
using System.Linq;

namespace GameObjects
{
    public class AgentFactory
    {

        private Dictionary<string, int> idMap = new Dictionary<string, int>();
        private List<GoapAction<GameCharacter>> bikerActions = new List<GoapAction<GameCharacter>>();
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

        public void AddBikerAction(GoapAction<GameCharacter> action)
        {
            bikerActions.Add(action);
        }   

        public GoapAgent<GameCharacter> CreateEnemyAgent(GameCharacter enemy)
        {
            string id = "enemy-" + idMap["enemy"];
            idMap["enemy"]++;


            var agent = new GoapAgent<GameCharacter>(id, enemy, new SimplePlanner<GameCharacter>());
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

        public GoapAgent<GameCharacter> CreateBikerAgent(GameCharacter biker)
        {
            List<GoapAction<GameCharacter>> actions = CloneBikerActions();
            string id = "biker-" + idMap["biker"];
            idMap["biker"]++;

            var agent = new GoapAgent<GameCharacter>(id, biker, new GoapPlanner<GameCharacter>());
            agent.SetActions(actions);

            return agent;
        }

        private List<GoapAction<GameCharacter>> CloneBikerActions()
        {
            return bikerActions.Select(action => action.Clone()).ToList();
        }
    }
}
