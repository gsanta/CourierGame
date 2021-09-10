
namespace Service.AI
{
    public interface IGoapAgentProvider<T>
    {
        GoapAgent<T> GetGoapAgent();
    }
}
