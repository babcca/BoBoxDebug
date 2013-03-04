using BoBox.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBox.Entities
{
    public abstract class Vertex : IVertex
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        public List<DummyVertex> Inputs { get; set; }
        public List<DummyVertex> Outputs { get; set; }        

        public abstract void Accept(IVertexVisitor visitor);
        public abstract TResult Accept<TResult>(IVertexVisitor<TResult> visitor);

        private IEnumerable<IVertex> successtors_;
        public IEnumerable<IVertex> Successtors { get { return successtors_; } }

        public void SetSuccesstors()
        {
            successtors_ = Outputs.Select(d => d.Next.Parent);
        }
        
    }
}
