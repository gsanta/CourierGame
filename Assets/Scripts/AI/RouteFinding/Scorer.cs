
namespace AI
{
    public interface Scorer<T>
    {
        float computeCost(T from, T to);
    }
}
