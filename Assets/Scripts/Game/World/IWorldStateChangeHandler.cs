
namespace Worlds
{
    public interface IWorldStateChangeHandler
    {
        void StateChanged();
        void SetWorldState(IMapState worldState);
    }
}
