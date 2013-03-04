using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBox.Entities.Interfaces
{
    public interface IAcceptVertexVisitor
    {
        void Accept(IVertexVisitor visitor);
        TResult Accept<TResult>(IVertexVisitor<TResult> visitor);
    }
}
