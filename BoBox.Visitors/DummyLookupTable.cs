using BoBox.Entities;
using BoBox.Entities.Interfaces;
using System.Diagnostics.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBox.Visitors
{
    /// <summary>
    /// Muze byt pouzita pouze jednou
    /// </summary>
    public class VertexToDummyCreateAndLookupTable : IVertexVisitor
    {
        public Dictionary<string, DummyVertex> DummyVertices { get; private set; }

        public VertexToDummyCreateAndLookupTable(Graph graph)
        {
            DummyVertices = new Dictionary<string, DummyVertex>();
            SetVertices(graph.Vertices);
        }

        private void SetVertices(List<IVertex> vertices)
        {
            Contract.Assert(vertices != null, "Vertices not loaded");            
            Contract.Assert(vertices.Count > 0, "Vertices are empty");

            foreach (var vertex in vertices)
            {
                vertex.Accept(this);
            }
        }

        public DummyVertex Lookup(string id)
        {
            DummyVertex dummy;
            if (DummyVertices.TryGetValue(id, out dummy))
            {
                return dummy;
            }

            throw new Exception(string.Format("Dummy vertex ({0}) not found", id));
        }

        public void Visit(Box visited)
        {

            foreach (var item in visited.Inputs)
            {
                try
                {
                    var input = new DummyVertex() { Id = item, Parent = visited };
                    visited.InputDummies.Add(input);
                    DummyVertices.Add(item, input);

                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Input dummy id isn't unique {0}", item), ex);
                }
            }

            foreach (var item in visited.Outputs)
            {
                try
                {
                    var dummy = new DummyVertex() { Id = item, Parent = visited };
                    visited.OutputDummies.Add(dummy);
                    DummyVertices.Add(item, dummy);
                }

                catch(Exception ex)
                {
                    throw new Exception(string.Format("Output dummy id isn't unique {0}", item), ex);
                }
            }

        }

        public void Visit(Subgraph visited)
        {
            foreach (var item in visited.Inputs)
            {
                var input = new DummyVertex() { Id = item, Parent = visited };
                visited.InputDummies.Add(input);
                DummyVertices.Add(item, input);                
            }

            foreach (var item in visited.Outputs)
            {
                var dummy = new DummyVertex() { Id = item, Parent = visited };
                visited.OutputDummies.Add(dummy);
                DummyVertices.Add(item, dummy);                
            }

            foreach (var vertex in visited.Vertices)
            {
                vertex.Accept(this);
            }
        }
    }
}
