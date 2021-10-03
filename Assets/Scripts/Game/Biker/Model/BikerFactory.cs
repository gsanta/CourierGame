using Delivery;
using Model;
using Pedestrians;
using Route;
using UnityEngine;
using Zenject;

namespace Bikers
{
    public class BikerFactory : MonoBehaviour, ItemFactory<BikerConfig, Biker>
    {
        [SerializeField]
        private MinimapBiker minimapTemplate;
        private BikerStore bikerStore;
        private IEventService eventService;
        private PackageStore packageStore;
        private IDeliveryService deliveryService;
        private InputHandler inputHandler;
        private AgentFactory agentFactory;

        [Inject]
        public void Construct(AgentFactory agentFactory, BikerStore bikerStore, IEventService eventService, PackageStore packageStore, IDeliveryService deliveryService, InputHandler inputHandler)
        {
            this.agentFactory = agentFactory;
            this.bikerStore = bikerStore;
            this.eventService = eventService;
            this.packageStore = packageStore;
            this.deliveryService = deliveryService;
            this.inputHandler = inputHandler;
        }

        public Biker Create(BikerConfig config)
        {
            Biker newBiker = Instantiate(bikerStore.BikerTemplate);
            newBiker.Construct(eventService, agentFactory);

            newBiker.GetComponent<BikerPlayComponent>().Construct(packageStore, inputHandler, deliveryService);

            newBiker.transform.position = config.spawnPoint.transform.position;
            newBiker.SetName(config.name);
            newBiker.gameObject.SetActive(true);

            MinimapBiker newMinimapBiker = Instantiate(minimapTemplate, minimapTemplate.transform.parent);
            newMinimapBiker.Biker = newBiker;
            newMinimapBiker.gameObject.SetActive(true);

            return newBiker;
        }
    }
}

