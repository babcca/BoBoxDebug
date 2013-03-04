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
        public Panel Transfrom(Graph model)
        {
            
            var vertices = ProcesVertices(model.Vertices);


            var canvas = new GraphCanvasControl();
            canvas.GraphVertex = vertices;

            //foreach (var item in vertices)
	        //{
            //    canvas.Children.Add(item);
	        //}

            return canvas;
        }

        public List<VertexControl> ProcesVertices(IList<IVertex> vertices)
        {
            List<VertexControl> controls = new List<VertexControl>();

            foreach (var vertex in vertices)
            {
                controls.Add(vertex.Accept<VertexControl>(this));
            }

            return controls;
        }

        public VertexControl Visit(Box visited)
        {
            return new BoxControl(visited);            
        }

        public VertexControl Visit(Subgraph visited)
        {
            var c = new SubgraphControl(visited);            
            c.Vertices = ProcesVertices(visited.Vertices);
            return c;
        }
    }
}
