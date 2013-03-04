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
        List<DummyVertex> Inputs { get; set; }
        List<DummyVertex> Outputs { get; set; }

        IEnumerable<IVertex> Successtors { get; }
        void SetSuccesstors();
    }
}
