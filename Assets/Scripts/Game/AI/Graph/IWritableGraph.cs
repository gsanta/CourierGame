namespace AI
{
    public interface IWritableGraph<in TNode, in TEdgeData>
    {
        void AddEdge(TEdgeData value);
    }
}
