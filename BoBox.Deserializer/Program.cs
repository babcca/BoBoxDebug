using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServiceStack.Text;
using BoBox.Entities;

namespace BoBox.Deserializer
{
    //public interface IVertexVisitor
    //{
    //    void Visit(Box visited);
    //    void Visit(Subgraph visited);
    //}

    //public interface IVertexVisitor<TReturn>
    //{
    //    TReturn Visit(Box visited);
    //    TReturn Visit(Subgraph visited);
    //}

    //public class VertexToDummyLookupTable : IVertexVisitor
    //{
    //    public Dictionary<string, DummyVertex> DummyVertices { get; private set; }

    //    public VertexToDummyLookupTable(List<IVertex> vertices)
    //    {
    //        DummyVertices = new Dictionary<string, DummyVertex>();
    //        SetVertices(vertices);
    //    }

    //    private void SetVertices(List<IVertex> vertices)
    //    {
    //        foreach (var vertex in vertices)
    //        {
    //            vertex.Accept(this);
    //        }
    //    }

    //    public DummyVertex Lookup(string id)
    //    {
    //        DummyVertex dummy;
    //        if (DummyVertices.TryGetValue(id, out dummy))
    //        {
    //            return dummy;
    //        }

    //        throw new Exception("Dummy vertex ({0}) not found".Fmt(id));
    //    }

    //    public void Visit(Box visited)
    //    {

    //        foreach (var item in visited.Inputs)
    //        {
    //            try
    //            {
    //                DummyVertices.Add(item.Id, item);

    //            }
    //            catch
    //            {
    //                throw new Exception("Dummy id isn't unique {0}".Fmt(item.Id));
    //            }
    //        }

    //        foreach (var item in visited.Outputs)
    //        {
    //            try
    //            {
    //                DummyVertices.Add(item.Id, item);

    //            }
    //            catch
    //            {
    //                throw new Exception("Dummy id isn't unique {0}".Fmt(item.Id));
    //            }
    //        }

    //    }

    //    public void Visit(Subgraph visited)
    //    {
    //        foreach (var item in visited.Inputs)
    //        {
    //            DummyVertices.Add(item.Id, item);
    //        }

    //        foreach (var item in visited.Outputs)
    //        {
    //            DummyVertices.Add(item.Id, item);
    //        }

    //        foreach (var vertex in visited.Vertices)
    //        {
    //            vertex.Accept(this);
    //        }
    //    }
    //}

    // json
    //public class Graph
    //{
    //    public List<IVertex> Vertices { get; set; }
    //    public List<Edge> Edges { get; set; }
    //}

    //// json
    //public class Edge
    //{
    //    public string Id { get; set; }
    //    public List<string> Path { get; set; }
    //}

    

    //public interface IAcceptVertexVisitor
    //{
    //    void Accept(IVertexVisitor visitor);
    //    TResult Accept<TResult>(IVertexVisitor<TResult> visitor);
    //}

    //public interface IVertex : IType, IAcceptVertexVisitor
    //{
    //    string Id { get; set; }
    //    string Label { get; set; }
    //    List<DummyVertex> Inputs { get; set; }
    //    List<DummyVertex> Outputs { get; set; }
    //}

    //public interface IType
    //{
    //    string Type { get; set; }
    //}

    //public class VertexType : IType
    //{
    //    public string Type { get; set; }
    //}

    //public abstract class Vertex : IVertex
    //{
    //    public string Id { get; set; }
    //    public string Label { get; set; }
    //    public string Type { get; set; }
    //    public List<DummyVertex> Inputs { get; set; }
    //    public List<DummyVertex> Outputs { get; set; }

    //    public abstract void Accept(IVertexVisitor visitor);
    //    public abstract TResult Accept<TResult>(IVertexVisitor<TResult> visitor);
    //}

    //public class Box : Vertex
    //{
    //    public override void Accept(IVertexVisitor visitor)
    //    {
    //        visitor.Visit(this);
    //    }

    //    public override TResult Accept<TResult>(IVertexVisitor<TResult> visitor)
    //    {
    //        return visitor.Visit(this);
    //    }
    //}



    //public class Subgraph : Vertex
    //{
    //    public List<IVertex> Vertices { get; set; }

    //    public override void Accept(IVertexVisitor visitor)
    //    {
    //        visitor.Visit(this);
    //    }

    //    public override TResult Accept<TResult>(IVertexVisitor<TResult> visitor)
    //    {
    //        return visitor.Visit(this);
    //    }
    //}

    //public class DummyVertex
    //{
    //    public string Id { get; set; }
    //    public IVertex Parent { get; set; }
    //    public DummyVertex Next { get; set; }
    //}

    class Program
    {
        static Graph GetModel()
        {
            var m = new Graph();
            m.Edges = new List<Edge>() { new Edge() { Id = "1", Path = new List<string>() { "2", "3" } } };

            return m;
        }

        static void Main(string[] args)
        {     
        }
    }


    class DeserializeTest
    {
        public void DeserializeEdgesTest()
        {
            var e = @"{""Edges"": [
		 { ""Id"": ""edge_from_b10_o"", ""Path"": [ ""b10_o"", ""b14_i"" ] },
		 { ""Id"": ""edge_from_b11_o"", ""Path"": [ ""b11_o"", ""b15_i"" ] },
		 { ""Id"": ""edge_from_b12_o"", ""Path"": [ ""b12_o"", ""b16_i"" ] },
		 { ""Id"": ""edge_from_b13_o"", ""Path"": [ ""b13_o"", ""b17_i"" ] },
		 { ""Id"": ""edge_from_b14_o"", ""Path"": [ ""b14_o"", ""b19_i"" ] },
		 { ""Id"": ""edge_from_b15_o"", ""Path"": [ ""b15_o"", ""b19_i"" ] },
		 { ""Id"": ""edge_from_b16_o"", ""Path"": [ ""b16_o"", ""b19_i"" ] },
		 { ""Id"": ""edge_from_b17_o"", ""Path"": [ ""b17_o"", ""b19_i"" ] },
		 { ""Id"": ""edge_from_b18_o"", ""Path"": [ ""b18_o"", ""b5_i"" ] },
		 { ""Id"": ""edge_from_b18_o"", ""Path"": [ ""b18_o"", ""b6_i"" ] },
		 { ""Id"": ""edge_from_b18_o"", ""Path"": [ ""b18_o"", ""b7_i"" ] },
		 { ""Id"": ""edge_from_b18_o"", ""Path"": [ ""b18_o"", ""b8_i"" ] },
		 { ""Id"": ""edge_from_b19_o"", ""Path"": [ ""b19_o"", ""b9_i"" ] },
		 { ""Id"": ""edge_from_b1_o"", ""Path"": [ ""b1_o"", ""b2_i"", ""b18_i"" ] },
		 { ""Id"": ""edge_from_b5_o"", ""Path"": [ ""b5_o"", ""b10_i"" ] },
		 { ""Id"": ""edge_from_b6_o"", ""Path"": [ ""b6_o"", ""b11_i"" ] },
		 { ""Id"": ""edge_from_b7_o"", ""Path"": [ ""b7_o"", ""b12_i"" ] },
		 { ""Id"": ""edge_from_b8_o"", ""Path"": [ ""b8_o"", ""b13_i"" ] },
		 { ""Id"": ""edge_from_b9_o"", ""Path"": [ ""b9_o"", ""b2_o"", ""b20_i"" ] }
	]}";

            var ed = JsonSerializer.DeserializeFromString<Graph>(e);

            System.Diagnostics.Debug.Assert(19 == ed.Edges.Count);
        }
    }
}
