using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AI
{
    public class DirectedGraph<TNode, TEdgeData>
    {
        private Dictionary<TNode, LinkedListCell<TNode>> adjacencyList = new Dictionary<TNode, LinkedListCell<TNode>>();
        private Dictionary<Tuple<TNode, TNode>, TEdgeData> edges = new Dictionary<Tuple<TNode, TNode>, TEdgeData>();
        private ISet<TNode> nodes = new HashSet<TNode>();

        public void AddEdge(TNode source, TNode dest, TEdgeData value)
        {
            Tuple<TNode, TNode> key = source != null ? new Tuple<TNode, TNode>(source, dest) : throw new ArgumentNullException();
            if (source.Equals(dest) || edges.ContainsKey(key))
            {
                throw new ArgumentException();
            }

            nodes.Add(source);
            nodes.Add(dest);

            edges.Add(key, value);
            LinkedListCell<TNode> linkedListCell;

            adjacencyList.TryGetValue(source, out linkedListCell);
            adjacencyList[source] = new LinkedListCell<TNode>()
            {
                Data = dest,
                Next = linkedListCell
            };

            if (!adjacencyList.ContainsKey(dest))
            {
                adjacencyList.Add(dest, null);
            }
        }

        public List<Tuple<TNode, TNode>> Edges { get => edges.Keys.ToList(); }

        public ISet<TNode> Nodes { get => nodes; }

        public bool ContainsNode(TNode node) => adjacencyList.ContainsKey(node);

        public IEnumerable<TNode> OutgoingNodes(TNode source) => new AdjacencyList(this, source);

        public class AdjacencyListEnumerator : IEnumerator<TNode>
        {
            private readonly DirectedGraph<TNode, TEdgeData> g;
            private TNode source;
            private LinkedListCell<TNode> current;
            private LinkedListCell<TNode> list = new LinkedListCell<TNode>();

            public AdjacencyListEnumerator(DirectedGraph<TNode, TEdgeData> g, TNode source)
            {
                this.g = g;
                this.source = source;
                list.Next = g.adjacencyList[source];
                current = list;
            }

            public TNode Current
            {
                get
                {
                    if (current == null || current == list)
                    {
                        throw new InvalidOperationException();
                    }

                    return current.Data;
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (current == null)
                {
                    return false;
                }

                current = current.Next;
                return current != null;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }

        public class AdjacencyList : IEnumerable<TNode>
        {
            private readonly DirectedGraph<TNode, TEdgeData> g;
            private readonly TNode source;

            public AdjacencyList(DirectedGraph<TNode, TEdgeData> g, TNode source)
            {
                this.g = g;
                this.source = source;
            }

            public IEnumerator<TNode> GetEnumerator() => new AdjacencyListEnumerator(g, source);

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}

