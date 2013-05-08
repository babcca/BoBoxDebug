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
        VertexToDummyCreateAndLookupTable DummyTable_;
        public VertexSuccestors(VertexToDummyCreateAndLookupTable dummyTable)
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
            // [TODO] snad zachovava poradi
            visited.Successors = visited.Outputs.Select(i => DummyTable_.Lookup(i).Next.Parent);
            visited.SuccesstorDumimes = visited.Outputs.Select(i => DummyTable_.Lookup(i).Next);

            visited.Ancestors = visited.Inputs.Select(i => DummyTable_.Lookup(i).Prev.Parent);
        }

        public void Visit(Entities.Subgraph visited)
        {
            visited.Successors = visited.Outputs.Select(i => DummyTable_.Lookup(i).Next.Parent);
            visited.SuccesstorDumimes = visited.Outputs.Select(i => DummyTable_.Lookup(i).Next);
            visited.Ancestors = visited.Inputs.Select(i => DummyTable_.Lookup(i).Prev.Parent);

            
            foreach (var item in visited.Vertices)
            {
                item.Accept(this);
            }
        }
    }    
}
