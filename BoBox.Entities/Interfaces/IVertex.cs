using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBox.Entities.Interfaces
{
    public interface IVertex : IType, IAcceptVertexVisitor
    {
        string Id { get; set; }
        string Label { get; set; }
        List<string> Inputs { get; set; }
        List<string> Outputs { get; set; }

        IEnumerable<IVertex> Successors { get; set; }
        IEnumerable<IVertex> Ancestors { get; set; }
        void AddSuccesstor(IVertex vertex);
    }
}
