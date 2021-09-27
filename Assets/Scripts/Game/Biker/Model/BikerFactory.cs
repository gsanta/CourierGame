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
        private PedestrianStore pedestrianStore;
        private Timer timer;
        private PackageStore2 packageStore2;
        private RouteAction routeAction;
        private RouteFacade routeFacade;

        [Inject]
        public void Construct(BikerStore bikerStore, PedestrianStore pedestrianStore, IEventService eventService, PackageStore packageStore, IDeliveryService deliveryService, InputHandler inputHandler, Timer timer, PackageStore2 packageStore2, RouteAction routeAction, RouteFacade routeFacade)
        {
            this.bikerStore = bikerStore;
            this.eventService = eventService;
            this.packageStore = packageStore;
            this.deliveryService = deliveryService;
            this.inputHandler = inputHandler;
            this.pedestrianStore = pedestrianStore;
            this.timer = timer;
            this.packageStore2 = packageStore2;
            this.routeAction = routeAction;
            this.routeFacade = routeFacade;
        }

        public Biker Create(BikerConfig config)
        {
            Biker newBiker = Instantiate(bikerStore.BikerTemplate);
            newBiker.Construct(eventService);

            newBiker.GetComponent<BikerAgentComponent>().Construct(packageStore, deliveryService, packageStore2, routeAction, routeFacade);
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

