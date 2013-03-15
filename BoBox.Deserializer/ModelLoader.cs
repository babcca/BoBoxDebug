using BoBox.Entities;
using BoBox.Entities.Interfaces;
using BoBox.Visitors;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoBox.Deserializer
{
    public class ModelLoader
    {
        Dictionary<string, Func<string, IVertex>> VertexFactory = new Dictionary<string, Func<string, IVertex>>();
        public ModelLoader()
        {
            VertexFactory.Add("box", json => JsonSerializer.DeserializeFromString<Box>(json));
            VertexFactory.Add("subgraph", json => JsonSerializer.DeserializeFromString<Subgraph>(json));

            JsConfig<IVertex>.RawDeserializeFn = DeserializeVertex;
        }

        public IVertex DeserializeVertex(string json)
        {            
            IType type = JsonSerializer.DeserializeFromString<VertexType>(json);
            IVertex vertex =  VertexFactory[type.Type.ToLower()](json);            

            return vertex;
        }

        public Graph LoadFromFile(string filename = "Data/q2.sparql.json")
        {                        
            var file = System.IO.File.ReadAllText(filename);

            var model = JsonSerializer.DeserializeFromString<Graph>(file);

            //// Add edges
            //var dummyLookupTable = new VertexToDummyLookupTable(model);
            //foreach (var edge in model.Edges)
            //{
            //    var source = dummyLookupTable.Lookup(edge.Path.First());                

            //    foreach (var dummy in edge.Path.Skip(1))
            //   {
            //        var v = dummyLookupTable.Lookup(dummy);
            //        source.Next = v;
            //        source = v;
            //    }
            //}

            //VertexSuccestors succ = new VertexSuccestors(dummyLookupTable);
            //succ.BuildSuccestors(model);

            return model;
        }
    }
}
