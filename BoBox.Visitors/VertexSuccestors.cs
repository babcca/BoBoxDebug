using BoBox.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBox.Visitors
{
    public class VertexSuccestors : IVertexVisitor
    {
        VertexToDummyLookupTable DummyTable_;
        public VertexSuccestors(VertexToDummyLookupTable dummyTable)
        {
            DummyTable_ = dummyTable;
        }

        public void BuildSuccestors(Entities.Graph graph)
        {
            foreach (var item in graph.Vertices)
            {
                item.Accept(this);
            }
        }

        public void Visit(Entities.Box visited)
        {
            visited.Successtors = visited.Outputs.Select(i => DummyTable_.Lookup(i).Next.Parent);
        }

        public void Visit(Entities.Subgraph visited)
        {
            visited.Successtors = visited.Outputs.Select(i => DummyTable_.Lookup(i).Next.Parent);

            foreach (var item in visited.Vertices)
            {
                item.Accept(this);
            }
        }
    }
}
