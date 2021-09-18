
namespace AI
{
    public class Edge<TNode, TEdgeData>
    {
        private TNode source;
        private TNode dest;
        private TEdgeData data;

        public TNode Source => source;
        public TNode Dest => dest;

        public TEdgeData Data => data;

        public Edge(TNode source, TNode dest, TEdgeData data)
        {
            this.source = source;
            this.dest = dest;
            this.data = data;
        }

    }
}

