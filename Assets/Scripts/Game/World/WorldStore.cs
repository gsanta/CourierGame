using System.Collections.Generic;

namespace Worlds
{
    public class WorldStore
    {
        private Dictionary<string, IWorldState> worlds = new Dictionary<string, IWorldState>();
        private IWorldState activeWorld;
        private readonly WorldHandlers worldHandlers;

        public WorldStore(WorldHandlers worldHandlers)
        {
            this.worldHandlers = worldHandlers;
        }

        public void AddWorld(WorldState worldState)
        {
            worlds.Add(worldState.Name, worldState);
        }

        public IWorldState GetWorld(string name)
        {
            return worlds[name];
        }

        public void SetActiveWorld(string name)
        {
            activeWorld = new WorldStateReporterDecorator(worlds[name], worldHandlers);
            worldHandlers.SetWorldState(activeWorld);
        }

        public IWorldState GetActiveWorld()
        {
            return activeWorld;
        }
    }
}
