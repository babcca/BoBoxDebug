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
        public List<string> Inputs { get; set; }
        public List<string> Outputs { get; set; }        

        public abstract void Accept(IVertexVisitor visitor);
        public abstract TResult Accept<TResult>(IVertexVisitor<TResult> visitor);

        
        public IEnumerable<IVertex> Successtors { get; set; }

        private List<IVertex> successtors_ = new List<IVertex>();
        public void AddSuccesstor(IVertex vertex)
        {
            successtors_.Add(vertex);
        }
    }
}
