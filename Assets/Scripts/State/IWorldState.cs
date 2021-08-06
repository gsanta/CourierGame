

public interface IWorldState
{
    public int SecondsPerDay();
    public bool IsMeasuring();
    public void SetMeasuring(bool isMeasuring);
}