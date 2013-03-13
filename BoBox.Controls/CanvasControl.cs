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
using System.Windows.Shapes;

namespace BoBox.Controls
{
    public class GraphCanvasControl : CanvasControl
    {
        public static readonly DependencyProperty GraphVertexProperty;
        public static readonly DependencyProperty GraphLayersProperty;

        public EdgeControl e;

        public List<VertexControl> GraphVertex
        {
            get { return GetValue(GraphVertexProperty) as List<VertexControl>; }
            set { SetValue(GraphVertexProperty, value); }
        }

        public StackPanel GraphLayers
        {
            get { return GetValue(GraphLayersProperty) as StackPanel; }
            set { SetValue(GraphLayersProperty, value); }
        }

        static GraphCanvasControl()
        {
            GraphVertexProperty = DependencyProperty.Register("GraphVertex", typeof(List<VertexControl>), typeof(GraphCanvasControl), new FrameworkPropertyMetadata(null, Vertex_PropertyChanged));

            GraphLayersProperty = DependencyProperty.Register("GraphLayers", typeof(StackPanel), typeof(GraphCanvasControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, Layers_PropertyChanged));
        }

        protected static void Layers_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var graph = d as GraphCanvasControl;
            graph.Children.Add(graph.GraphLayers);
            //graph.UpdateLayout();
            //foreach (var layer in graph.GraphLayers.Children)
            //{
            //    var l = new Line();
            //    l.Stroke = Brushes.Black;
            //    l.X1 = 100;
            //    l.Y1 = 10;

            //    l.Y2 = 100;
            //    l.Y2 = 100;

            //    graph.Children.Add(l);
            //}
            //graph.e = new EdgeControl() { Source = ((VertexControl)(((StackPanel)((StackPanel)graph.Children[0]).Children[1]).Children[0])).Input[0], Target = ((VertexControl)(((StackPanel)((StackPanel)graph.Children[0]).Children[1]).Children[0])).Output[0] };            
            //graph.Children.Add(graph.e);            
        }

        protected static void Vertex_PropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            //e.RenderLine();
        }


    }


    public class CanvasControl : Panel
    {
        // http://msdn.microsoft.com/en-us/library/ms754152.aspx#Panels_custom_panel_elements
        /// <summary>
        /// Slouzi pro urceni velikosti plochy
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size constraint)
        {
            double sizeX = 0;
            double sizeY = 0;

            foreach (UIElement child in InternalChildren)
            {
                child.Measure(constraint);
                sizeX = Math.Max(sizeX, child.DesiredSize.Width);
                sizeY = Math.Max(sizeY, child.DesiredSize.Height);
            }

            return new Size(sizeX, sizeY);
        }

        /// <summary>
        /// Slouzi pro rozmisteni prvku v plose
        /// </summary>
        /// <param name="arrangeSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                child.Arrange(new Rect(new Point(0, 0), child.DesiredSize));
            }

            return arrangeSize; // Returns the final Arranged size
        }
    }
}
