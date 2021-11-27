using System.Collections.Generic;

namespace Worlds
{
    public class WorldStore
    {
        private Dictionary<string, IMapState> worlds = new Dictionary<string, IMapState>();
        private IMapState activeWorld;
        private WorldHandlers worldHandlers;

        public string CurrentMap { get; set; } = "Map1";

        // avoiding circular deps it is passed here instead of in the constructor
        public void SetWorldHandlers(WorldHandlers worldHandlers)
        {
            this.worldHandlers = worldHandlers;
        }

        public void AddWorld(MapState worldState)
        {
            worlds.Add(worldState.Name, worldState);
        }

        public IMapState GetWorld(string name)
        {
            return worlds[name];
        }

        public void SetActiveWorld(string name)
        {
            activeWorld = new WorldStateReporterDecorator(worlds[name], worldHandlers);
            worldHandlers.SetWorldState(activeWorld);
        }

        public IMapState GetActiveWorld()
        {
            return activeWorld;
        }
    }
}
