
using System.Collections.Generic;

namespace Worlds
{
    public class WorldHandlers
    {
        public readonly CurfewHandler curfewHandler;
        private List<IWorldStateChangeHandler> handlers = new List<IWorldStateChangeHandler>();

        public WorldHandlers(CurfewHandler curfewHandler)
        {
            this.curfewHandler = curfewHandler;
            handlers.Add(curfewHandler);
        }

        public void SetWorldState(IWorldState worldState)
        {
            handlers.ForEach(handler => handler.SetWorldState(worldState));
        }
    }
}
