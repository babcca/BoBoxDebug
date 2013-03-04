﻿using BoBox.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BoBox.Visitors
{
    public class ProprtiesLookupTable<TProprties> : IVertexVisitor
        where TProprties : new()
    {

        public static ProprtiesLookupTable<TProprties> Build(IEnumerable<IVertex> vertices)
        {
            var table = new ProprtiesLookupTable<TProprties>();

            foreach (var vertex in vertices)
            {
                vertex.Accept(table);
            }

            return table;
        }

        public TProprties Get(string key)
        {
            TProprties value;
            if (!table_.TryGetValue(key, out value))
            {
                throw new Exception(string.Format("Key {0} not found", key));
            }
            return value;
        }

        private Dictionary<string, TProprties> table_ = new Dictionary<string, TProprties>();
        public IDictionary<string, TProprties> Table { get { return table_; } }

        public void Visit(Entities.Box visited)
        {
            table_.Add(visited.Id, new TProprties());
        }

        public void Visit(Entities.Subgraph visited)
        {
            table_.Add(visited.Id, new TProprties());

            foreach (var vertex in visited.Vertices)
            {
                vertex.Accept(this);
            }
        }
    }

    public class PositionAssign : IVertexVisitor<Point>
    {
        ProprtiesLookupTable<LayeringProperties> verticesLayer;
        Dictionary<string, ProprtiesLookupTable<LayeringProperties>> subgraphsLayer;

        public Point Visit(Entities.Box visited)
        {
            throw new NotImplementedException();
        }

        public Point Visit(Entities.Subgraph visited)
        {
            throw new NotImplementedException();
        }
    }


    public class LayeringProperties
    {
        public enum Colors { White, Grey, Black };

        public Colors Color { get; set; }
        public int Layer { get; set; }

        public LayeringProperties()
        {
            Color = Colors.White;
            Layer = 0;
        }
    }

    public class TopologicalNumbers : IVertexVisitor<int>
    {
        ProprtiesLookupTable<LayeringProperties> properties;
        Dictionary<string, ProprtiesLookupTable<LayeringProperties>> subgraphLayering = new Dictionary<string, ProprtiesLookupTable<LayeringProperties>>();



        public Dictionary<int, List<IVertex>> ComputeLayers(IEnumerable<IVertex> vertices, IEnumerable<IVertex> sinks)
        {
            properties = ProprtiesLookupTable<LayeringProperties>.Build(vertices);

            // Posledni vrcholy jsou v prvni vrstve
            foreach (var sink in sinks)
            {
                properties.Get(sink.Id).Color = LayeringProperties.Colors.Black;
            }

            // Rekurzivne dopocitej vrsty pro ostatni vrcholy
            // [TODO] Nejlepe pro source (v podgrafech to jsou Intputs)
            foreach (var vertex in vertices)
            {
                vertex.Accept(this);
            }

            Dictionary<int, List<IVertex>> layers = new Dictionary<int, List<IVertex>>();

            foreach (var item in properties.Table)
            {
                List<IVertex> layer;
                if (!layers.TryGetValue(item.Value.Layer, out layer))
                {

                }
                else
                {
                    layer.Add(vertices.First(v => v.Id == item.Key));
                }
            }

            return layers;
        }

        public int Visit(Entities.Box visited)
        {
            var me = properties.Get(visited.Id);

            // Vstoupili jsme do nezpracovaneho vrcholu, zpracuj a vrat jeho vrstvu
            if (me.Color != LayeringProperties.Colors.Black)
            {

                me.Layer = GetLayer(visited);
                me.Color = LayeringProperties.Colors.Black;
            }

            return me.Layer;

        }

        public int Visit(Entities.Subgraph visited)
        {
            var me = properties.Get(visited.Id);

            // Vstoupili jsme do nezpracovaneho vrcholu, zpracuj a vrat jeho vrstvu
            if (me.Color != LayeringProperties.Colors.Black)
            {
                me.Layer = GetLayer(visited);
                me.Color = LayeringProperties.Colors.Black;

                // Rozvrstvi vrcholy podgrafu
                // Nemusime poctat hned, klidne odsunout az bude potreba nebo paralelne
                // NEROZUMET
                var sinks = visited.Vertices.Where(v => v.Outputs.Count == 1 && v.Outputs.Select(o => o.Next.Parent).Contains(visited));
                var b = new TopologicalNumbers();
                b.ComputeLayers(visited.Vertices, sinks);
            }
            return me.Layer;
        }

        private int GetLayer(IVertex vertex)
        {
            int newLayer = vertex.Successtors.Select(s => s.Accept(this)).Max() + 1;
            return newLayer;
        }
    }
}
