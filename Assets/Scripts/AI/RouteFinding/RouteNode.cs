using AI;
using System;

public class RouteNode<T> : IComparable<RouteNode<T>> where T : class, IMonoBehaviour
{
    private readonly T current;
    private T previous;
    private double routeScore;
    private double estimatedScore;

    public RouteNode(T current) : this(current, null, Double.PositiveInfinity, Double.PositiveInfinity)
    {

    }

    public RouteNode(T current, T previous, double routeScore, double estimatedScore)
    {
        this.current = current;
        this.previous = previous;
        this.routeScore = routeScore;
        this.estimatedScore = estimatedScore;
    }

    public int CompareTo(RouteNode<T> other)
    {
        if (this.estimatedScore > other.estimatedScore)
        {
            return 1;
        }
        else if (this.estimatedScore < other.estimatedScore)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    public T Current { get => current; }
    public T Previous { get => previous; set => previous = value; }
    public double RouteScore { get => routeScore; set => routeScore = value; }
    public double EstimatedScore { get => estimatedScore; set => estimatedScore = value; }
}
