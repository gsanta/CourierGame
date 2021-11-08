
namespace Worlds
{
    public class WorldStateReporterDecorator : IMapState
    {
        private readonly IMapState baseState;
        private readonly WorldHandlers worldHandlers;

        public WorldStateReporterDecorator(IMapState baseState, WorldHandlers worldHandlers)
        {
            this.baseState = baseState;
            this.worldHandlers = worldHandlers;
        }

        public bool Curfew {
            get {
                return baseState.Curfew;
            }
            set {
                baseState.Curfew = value;
                worldHandlers.curfewHandler.StateChanged();
            } 
        }
        public string Name { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}
