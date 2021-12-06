using Delivery;
using Service;

namespace GameObjects
{
    public class BikerFactory : ItemFactory<BikerConfig, GameCharacter>
    {
        private EventService eventService;
        private DeliveryService deliveryService;
        private AgentFactory agentFactory;
        private IBikerInstantiator bikerInstantiator;

        public BikerFactory(AgentFactory agentFactory, EventService eventService, DeliveryService deliveryService)
        {                                                                                                          
            this.agentFactory = agentFactory;
            this.eventService = eventService;
            this.deliveryService = deliveryService;
        }

        public void SetBikerInstantiator(IBikerInstantiator bikerInstantiator)
        {
            this.bikerInstantiator = bikerInstantiator;
        }

        public GameCharacter Create(BikerConfig config)
        {

            GameCharacter biker = bikerInstantiator.InstantiateBiker();
            biker.Agent = agentFactory.CreateBikerAgent(biker);
            biker.GoalProvider = new BikerGoalProvider(biker);

            biker.transform.position = config.spawnPoint.transform.position;
            biker.SetName(config.name);
            biker.gameObject.SetActive(true);

            //MinimapBiker newMinimapBiker = bikerInstantiator.InstantiateMinimapBiker();
            //newMinimapBiker.Biker = biker;
            //newMinimapBiker.gameObject.SetActive(true);

            return biker;
        }
    }
}

