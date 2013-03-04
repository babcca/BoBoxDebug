using BoBox.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBox.Entities
{
    public class Box : Vertex
    {
        public override void Accept(IVertexVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult>(IVertexVisitor<TResult> visitor)
        {
            return visitor.Visit(this);
        }
    }

    /*
    public class Dummy : Vertex
    {
        public override void Accept(IVertexVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult>(IVertexVisitor<TResult> visitor)
        {
            return visitor.Visit(this);
        }
    }
    */

    public class Subgraph : Vertex
    {
        public List<IVertex> Vertices { get; set; }

        public override void Accept(IVertexVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override TResult Accept<TResult>(IVertexVisitor<TResult> visitor)
        {
            return visitor.Visit(this);
        }

        public IEnumerable<IVertex> GetSinks()
        {
            return this.Vertices.Where(v => v.Successtors.Contains(this));
        }
    }

    public class DummyVertex
    {
        public string Id { get; set; }
        public IVertex Parent { get; set; }
        public DummyVertex Next { get; set; }
    }

}
