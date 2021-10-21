using Delivery;
using Service;

namespace Bikers
{
    public class BikerFactory : ItemFactory<BikerConfig, Biker>
    {
        private EventService eventService;
        private PackageStore packageStore;
        private DeliveryService deliveryService;
        private InputHandler inputHandler;
        private AgentFactory agentFactory;
        private IBikerInstantiator bikerInstantiator;

        public BikerFactory(AgentFactory agentFactory, EventService eventService, PackageStore packageStore, DeliveryService deliveryService, InputHandler inputHandler)
        {
            this.agentFactory = agentFactory;
            this.eventService = eventService;
            this.packageStore = packageStore;
            this.deliveryService = deliveryService;
            this.inputHandler = inputHandler;
        }

        public void SetBikerInstantiator(IBikerInstantiator bikerInstantiator)
        {
            this.bikerInstantiator = bikerInstantiator;
        }

        public Biker Create(BikerConfig config)
        {
            Biker newBiker = bikerInstantiator.InstantiateBiker();
            newBiker.Construct(eventService, agentFactory);

            newBiker.GetComponent<BikerPlayComponent>().Construct(inputHandler, deliveryService);

            newBiker.transform.position = config.spawnPoint.transform.position;
            newBiker.SetName(config.name);
            newBiker.gameObject.SetActive(true);

            MinimapBiker newMinimapBiker = bikerInstantiator.InstantiateMinimapBiker();
            newMinimapBiker.Biker = newBiker;
            newMinimapBiker.gameObject.SetActive(true);

            return newBiker;
        }
    }
}

