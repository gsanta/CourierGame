using AI;
using System;

class TestScorer : Scorer<IntVec>
{
    public float computeCost(IntVec from, IntVec to)
    {
        return (float) Math.Sqrt((from.x - to.x) * (from.x - to.x) + (from.y - to.y) * (from.y - to.y));
    }
}
