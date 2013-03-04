using BoBox.Entities;
using BoBox.Entities.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBox.Visitors
{
    public class VertexToDummyLookupTable : IVertexVisitor
    {
        public Dictionary<string, DummyVertex> DummyVertices { get; private set; }

        public VertexToDummyLookupTable(List<IVertex> vertices)
        {
            DummyVertices = new Dictionary<string, DummyVertex>();
            SetVertices(vertices);
        }

        private void SetVertices(List<IVertex> vertices)
        {
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
                    DummyVertices.Add(item.Id, item);

                }
                catch
                {
                    throw new Exception(string.Format("Dummy id isn't unique {0}", item.Id));
                }
            }

            foreach (var item in visited.Outputs)
            {
                try
                {
                    DummyVertices.Add(item.Id, item);

                }
                catch
                {
                    throw new Exception(string.Format("Dummy id isn't unique {0}", item.Id));
                }
            }

        }

        public void Visit(Subgraph visited)
        {
            foreach (var item in visited.Inputs)
            {
                DummyVertices.Add(item.Id, item);
            }

            foreach (var item in visited.Outputs)
            {
                DummyVertices.Add(item.Id, item);
            }

            foreach (var vertex in visited.Vertices)
            {
                vertex.Accept(this);
            }
        }
    }

}
