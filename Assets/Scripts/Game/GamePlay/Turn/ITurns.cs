using RSG;

namespace GamePlay
{
    public interface ITurns
    {
        void Reset();
        void Abort();
        Promise Execute();
        void Step();

        void Pause();
        void Resume();
    }
}
