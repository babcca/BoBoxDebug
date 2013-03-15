using BoBox.Controls;
using BoBox.Controls.Vertices;
using BoBox.Entities;
using BoBox.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;



namespace BoBox.Visitors
{

   

    public class ModelToControl : IVertexVisitor<VertexControl>
    {
        public static Dictionary<DummyVertex, DummyControl> CreatedControls = new Dictionary<DummyVertex, DummyControl>();
        public bool Debug { get; set; }
        protected StackPanel Transfrom(List<IVertex> vertices, IEnumerable<IVertex> sinks)
        {
            
            VerticesLayering LayeringProcessor = new VerticesLayering();
            var la = LayeringProcessor.ComputeLayers(vertices, sinks);

            StackPanel msp = new StackPanel();            
            for (int i = la.Count - 1; i >= 0; --i)
            {
                System.Diagnostics.Debug.WriteLine(i);
                msp.Children.Add(ProcesVertices(la[i]));
                
            }
            if (Debug)
            {
                msp.Background = Brushes.Red;
            }
            //foreach (var layer in la)
            //{
            //    System.Diagnostics.Debug.WriteLine(layer.Key);
            //    msp.Children.Add(ProcesVertices(layer.Value));
            //}

            //var vertices = ProcesVertices(model.Vertices);


            //var canvas = new GraphCanvasControl();
            //canvas.GraphVertex = vertices;

            //foreach (var item in vertices)
            //{
            //    canvas.Children.Add(item);
            //}
            var canvas = new BoBox.Controls.GraphCanvasControl();
            //canvas.GraphLayers = 
            return msp;
        }
        public StackPanel Transfrom(Graph model)
        {
            Debug = false;
            DummyLookupTable = new VertexToDummyCreateAndLookupTable(model);
            // Add edges            
            foreach (var edge in model.Edges)
            {
                var source = DummyLookupTable.Lookup(edge.Path.First());

                foreach (var dummy in edge.Path.Skip(1))
                {
                    var v = DummyLookupTable.Lookup(dummy);
                    source.Next = v;
                    source = v;
                }
            }

            VertexSuccestors succ = new VertexSuccestors(DummyLookupTable);
            succ.BuildSuccestors(model);



            return Transfrom(model.Vertices, model.Vertices.Where(v => v.Outputs.Count == 0));
        }

        public Panel ProcesVertices(IList<IVertex> vertices)
        {
            //List<VertexControl> controls = new List<VertexControl>();
            StackPanel controls = new StackPanel() { Orientation = System.Windows.Controls.Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(0, 20, 0, 20) };

            byte c = 0;
            foreach (var vertex in vertices)
            {
                controls.Children.Add(vertex.Accept<VertexControl>(this));
                ++c;
            }
            
            if (Debug)
            {
                controls.Background = new SolidColorBrush(Color.FromRgb((byte)(10 * c), (byte)(10 * c), (byte)( 10 * c)));
            }
            return controls;
        }

        public VertexControl Visit(Box visited)
        {
            var box = new BoxControl() { Label = visited.Label };
                       
            foreach (var item in visited.InputDummies)
            {
                var d = GetDummyControl(item);                
                box.Input.Add(d);                 
                
            }

            foreach (var item in visited.OutputDummies)
            {                                
                var v = GetDummyControl(item);
                v.Next = GetDummyControl(item.Next);
                box.Output.Add(v);                                
            }



            
            return box;
        }        

        private DummyControl GetDummyControl(DummyVertex dummy)
        {
            DummyControl ret;
            if (!CreatedControls.TryGetValue(dummy, out ret))
            {
                ret = new DummyControl() { Id = dummy.Parent.Label };
                CreatedControls.Add(dummy, ret);
            }

            return ret;


        }

        public VertexControl Visit(Subgraph visited)
        {
            var box = new SubgraphControl() { Label = visited.Label };

            //var a = new ModelToControl();
            var sinks = visited.GetSinks();
            box.GraphLayers = Transfrom(visited.Vertices, sinks);

            foreach (var item in visited.InputDummies)
            {
                var v = GetDummyControl(item);
                v.Next = GetDummyControl(item.Next);

                box.Input.Add(v);


            }

            foreach (var item in visited.OutputDummies)
            {
                var v = GetDummyControl(item);
                v.Next = GetDummyControl(item.Next);
                box.Output.Add(v);
            }
            //c.OnFirstExpand = v => ;
            //visited.Vertices = 
            return box;
        }

        public VertexToDummyCreateAndLookupTable DummyLookupTable { get; set; }
    }
}
