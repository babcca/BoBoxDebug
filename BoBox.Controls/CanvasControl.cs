using BoBox.Entities;
using BoBox.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BoBox.Controls
{
    public class GraphCanvasControl : CanvasControl
    {
        public static readonly DependencyProperty GraphVertexProperty;

        public List<VertexControl> GraphVertex
        {
            get { return GetValue(GraphVertexProperty) as List<VertexControl>; }
            set { SetValue(GraphVertexProperty, value); }
        }

        static GraphCanvasControl()
        {
            GraphVertexProperty = DependencyProperty.Register("GraphVertex", typeof(List<VertexControl>), typeof(GraphCanvasControl), new FrameworkPropertyMetadata(null, Vertex_PropertyChanged));
        }

        protected static void Vertex_PropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var graph = obj as GraphCanvasControl;
            //var self = (GraphLayout<TGraph>)obj;
            //self.OnGraphUpdate();


            var panel = new StackPanel();

            var layer = new LayerPanel();
            for (int i = 0; i < graph.GraphVertex.Count; ++i)
            {
                var l = new List<VertexControl>();
                l.Add(graph.GraphVertex.ElementAt(i));
                layer.Vertices = l;
                panel.Children.Add(layer);
                layer = new LayerPanel();
            }


            graph.Children.Add(panel);

        }


    }


    public class CanvasControl : Panel
    {

        public CanvasControl()
        {
        }

        // http://msdn.microsoft.com/en-us/library/ms754152.aspx#Panels_custom_panel_elements
        /// <summary>
        /// Slouzi pro urceni velikosti plochy
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        //protected override Size MeasureOverride(Size constraint)
        //{
        //    double sizeX = 0;
        //    double sizeY = 0;

        //    foreach (UIElement child in InternalChildren)
        //    {
        //        child.Measure(constraint);
        //        if (child is VertexControl)
        //        {
        //            var vertex = (VertexControl)child;

        //            sizeX = Math.Max(sizeX, child.DesiredSize.Width + vertex.X);
        //            sizeY = Math.Max(sizeY, child.DesiredSize.Height + vertex.Y);
        //        }
        //    }

        //    return new Size(sizeX, sizeY);
        //}

        /// <summary>
        /// Slouzi pro rozmisteni prvku v plose
        /// </summary>
        /// <param name="arrangeSize"></param>
        /// <returns></returns>
        //protected override Size ArrangeOverride(Size arrangeSize)
        //{
        //    foreach (UIElement child in InternalChildren)
        //    {
        //        if (child is VertexControl)
        //        {
        //            var vertex = (VertexControl)child;
        //            vertex.Arrange(new Rect(new Point(vertex.X, vertex.Y), child.DesiredSize));
        //        }
        //        else
        //        {

        //        }
        //    }

        //    return arrangeSize; // Returns the final Arranged size
        //}
    }
}
