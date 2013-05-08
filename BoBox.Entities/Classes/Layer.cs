using BoBox.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBox.Entities.Classes
{
    public class Layer
    {
        public int Index { get; private set; }
        public List<IVertex> Vertices { get; set; }

    }

    public class LayerList
    {
        private Dictionary<int, List<IVertex>> layers_ = new Dictionary<int, List<IVertex>>();

        public Dictionary<int, List<IVertex>> Layers { get { return layers_;}  }

        public int MaxWidth { get { return layers_.Max(l => l.Value.Count); } }

        public void InsertToLayer(int layerIndex, IVertex vertex)
        {
            List<IVertex> layer;
            layers_.TryGetValue(layerIndex, out layer);

            if (layer != default(List<IVertex>))
            {
                layer.Add(vertex);    
            }
            else
            {
                var newLayer = new List<IVertex>();
                layers_[layerIndex] = newLayer;
                // add to right column
                newLayer.Add(vertex);
            }            
        }
    }
}
