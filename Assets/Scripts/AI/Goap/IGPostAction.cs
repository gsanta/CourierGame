namespace AI
{
    public interface IGPostAction
    {
        void Execute();
        IGPostAction Clone();
    }
}
