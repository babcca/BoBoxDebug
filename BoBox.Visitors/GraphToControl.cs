using BoBox.Controls;
using BoBox.Controls.Vertices;
using BoBox.Entities;
using BoBox.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;



namespace BoBox.Visitors
{
    public class ModelToControl : IVertexVisitor<VertexControl>
    {

        protected StackPanel Transfrom(List<IVertex> vertices, IEnumerable<IVertex> sinks)
        {
            VerticesLayering LayeringProcessor = new VerticesLayering();
            var la = LayeringProcessor.ComputeLayers(vertices, sinks);

            StackPanel msp = new StackPanel();
            foreach (var layer in la)
            {
                msp.Children.Add(ProcesVertices(layer.Value));
            }

            //var vertices = ProcesVertices(model.Vertices);


            //var canvas = new GraphCanvasControl();
            //canvas.GraphVertex = vertices;

            //foreach (var item in vertices)
            //{
            //    canvas.Children.Add(item);
            //}

            return msp;
        }
        public StackPanel Transfrom(Graph model)
        {
            foreach (var item in model.Edges)
            {
                //item.Path
            }


            return Transfrom(model.Vertices, model.Vertices.Where(v => v.Outputs.Count == 0));
        }

        public Panel ProcesVertices(IList<IVertex> vertices)
        {
            //List<VertexControl> controls = new List<VertexControl>();
            StackPanel controls = new StackPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal };

            foreach (var vertex in vertices)
            {
                controls.Children.Add(vertex.Accept<VertexControl>(this));
            }

            return controls;
        }

        public VertexControl Visit(Box visited)
        {
            var box = new BoxControl(visited);
            foreach (var item in visited.Inputs)
            {
                box.Input.Add(new DummyControl());
            }
            return box;
        }

        public VertexControl Visit(Subgraph visited)
        {
            var c = new SubgraphControl(visited);
            //var a = new ModelToControl();
            var sinks = visited.GetSinks();
            c.GraphLayers = Transfrom(visited.Vertices, sinks);
            //c.OnFirstExpand = v => ;
            //visited.Vertices = 
            return c;
        }
    }
}
