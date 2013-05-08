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
            return this.Vertices.Where(v => v.Successors.Contains(this));
        }
    }

    public class DummyVertex
    {
        public string Id { get; set; }
        
        /// <summary>
        /// Nactena data k vrcholu
        /// </summary>
        public IVertex Parent { get; set; }
        
        /// <summary>
        /// Nasledujici Dummy vrchol
        /// </summary>        
        public DummyVertex Next { get; set; }

        /// <summary>
        /// Predchozi dummy vrchol
        /// </summary>
        public DummyVertex Prev { get; set; }

        /// <summary>
        /// WPF Control objektu ktery se vykresluje
        /// </summary>
        public IDummyControl Control { get; set; }
    }

}
