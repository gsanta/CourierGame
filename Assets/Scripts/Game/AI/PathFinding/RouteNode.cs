using System;

public class RouteNode<T> : IComparable
{
    private readonly T current;
    private T previous;
    private double routeScore;
    private double estimatedScore;

    public RouteNode(T current): this(current, default(T), Double.PositiveInfinity, Double.PositiveInfinity)
    {

    }

    public RouteNode(T current, T previous, double routeScore, double estimatedScore)
    {
        this.current = current;
        this.previous = previous;
        this.routeScore = routeScore;
        this.estimatedScore = estimatedScore;
    }

    public int CompareTo(object obj)
    {
        throw new NotImplementedException();
    }
}
