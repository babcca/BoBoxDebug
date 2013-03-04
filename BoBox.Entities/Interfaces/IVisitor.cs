using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBox.Entities.Interfaces
{
    public interface IVertexVisitor<TReturn>
    {
        TReturn Visit(Box visited);
        TReturn Visit(Subgraph visited);
    }

    public interface IVertexVisitor
    {
        void Visit(Box visited);
        void Visit(Subgraph visited);
    }
}
