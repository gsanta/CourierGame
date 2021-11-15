using RSG;

namespace GamePlay
{
    public interface ITurns
    {
        void Reset();
        Promise Execute();
        void Step();
    }
}
