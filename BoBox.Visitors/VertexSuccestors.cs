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
            visited.Successtors = visited.Outputs.Select(i =>
                {
                    var ancestor = DummyTable_.Lookup(i);
                    var successtor = ancestor.Next.Parent;
                    //ancestor.Next
                    //visited.

                    return successtor;
                }
            );
            visited.SuccesstorDumimes = visited.Outputs.Select(i => DummyTable_.Lookup(i).Next);            
        }

        public void Visit(Entities.Subgraph visited)
        {
            visited.Successtors = visited.Outputs.Select(i => DummyTable_.Lookup(i).Next.Parent);
            visited.SuccesstorDumimes = visited.Outputs.Select(i => DummyTable_.Lookup(i).Next);

            foreach (var item in visited.Vertices)
            {
                item.Accept(this);
            }
        }
    }    
}
