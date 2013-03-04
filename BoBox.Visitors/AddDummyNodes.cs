using BoBox.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBox.Visitors
{
    class AddDummyNodes : IVertexVisitor
    {
        ProprtiesLookupTable<LayeringProperties> LayeringData { get; private set; }
        Dictionary<int, List<IVertex>> Layers { get; private set; }

        public AddDummyNodes(Dictionary<int, List<IVertex>> layers, ProprtiesLookupTable<LayeringProperties> layeringProperties)
        {
            var sources = layers[layers.Count];

            foreach (var u in sources)
            {
                foreach (var v in u.Successtors)
                {
                    if (VertexSpan(u, v) > 1)
                    {
                        AddDummies(u, v);
                    }
                }
            }
        }

        private void AddDummies(IVertex u, IVertex v)
        {
            for (int sourceLayer = LayeringData.Get(u.Id).Layer - 1; sourceLayer > LayeringData.Get(v.Id).Layer; --sourceLayer)
            {
                Entities.Dummy dummy = new Entities.Dummy();
                dummy.Id = string.Format("Dummy_{0}_{1}", u.Id, v.Id);
                dummy.Label = string.Format("Dummy_{0}_{1}", u.Id, v.Id);
                
                //var p = u.Outputs.First(d => d.Parent == v)
                    
                //Layers[sourceLayer]
                p.Parent = dummy;
            }
            
        }

        private int VertexSpan(IVertex u, IVertex v)
        {
            return LayeringData.Get(u.Id).Layer - LayeringData.Get(v.Id).Layer;
        }

        public void Visit(Entities.Box visited)
        {
            throw new NotImplementedException();
        }

        public void Visit(Entities.Subgraph visited)
        {
            throw new NotImplementedException();
        }
    }
}
