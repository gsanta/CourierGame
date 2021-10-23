using Delivery;
using Service;

namespace Bikers
{
    public class BikerFactory : ItemFactory<BikerConfig, Biker>
    {
        private EventService eventService;
        private DeliveryService deliveryService;
        private InputHandler inputHandler;
        private AgentFactory agentFactory;
        private IBikerInstantiator bikerInstantiator;

        public BikerFactory(AgentFactory agentFactory, EventService eventService, DeliveryService deliveryService, InputHandler inputHandler)
        {
            this.agentFactory = agentFactory;
            this.eventService = eventService;
            this.deliveryService = deliveryService;
            this.inputHandler = inputHandler;
        }

        public void SetBikerInstantiator(IBikerInstantiator bikerInstantiator)
        {
            this.bikerInstantiator = bikerInstantiator;
        }

        public Biker Create(BikerConfig config)
        {

            Biker biker = bikerInstantiator.InstantiateBiker();
            biker.EventService = eventService;
            biker.Agent = agentFactory.CreateBikerAgent(biker);
            biker.GoalProvider = new BikerGoalProvider(biker);

            biker.GetComponent<BikerPlayComponent>().Construct(inputHandler, deliveryService);

            biker.transform.position = config.spawnPoint.transform.position;
            biker.SetName(config.name);
            biker.gameObject.SetActive(true);

            MinimapBiker newMinimapBiker = bikerInstantiator.InstantiateMinimapBiker();
            newMinimapBiker.Biker = biker;
            newMinimapBiker.gameObject.SetActive(true);

            return biker;
        }
    }
}

