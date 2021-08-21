

public interface IWorldState
{
    public int SecondsPerDay();
    public bool IsDayStarted();
    public void StartDay();
    public void EndDay();
}