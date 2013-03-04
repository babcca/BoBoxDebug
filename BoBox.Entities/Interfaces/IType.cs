using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBox.Entities.Interfaces
{
    public interface IType
    {
        string Type { get; set; }
    }

    public class VertexType : IType
    {
        public string Type { get; set; }
    }

}
