using BoBox.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBox.Entities
{
    // json
    public class Graph
    {
        public List<IVertex> Vertices { get; set; }
        public List<Edge> Edges { get; set; }
    }
}
