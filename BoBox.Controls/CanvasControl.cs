using BoBox.Controls.Vertices;
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

        protected static void change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var a = "A";
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

            graph.GraphVertex = new List<VertexControl>();
            foreach (var item in graph.GraphLayers.Children)
            {
                var l = item as StackPanel;
                var z = l.Children.Cast<VertexControl>();
                graph.GraphVertex.AddRange(z);

            }
        }

        public GraphCanvasControl()
        {
            //this.LayoutUpdated += GraphCanvasControl_LayoutUpdated;
            //this.ManipulationCompleted += GraphCanvasControl_ManipulationCompleted;
            this.SizeChanged += GraphCanvasControl_SizeChanged;

            //this.RequestBringIntoView += GraphCanvasControl_RequestBringIntoView;                        
        }

        void GraphCanvasControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (var v in GraphVertex)
            {

                if (v is SubgraphControl)
                {
                    var s = v as SubgraphControl;
                    foreach (var item in s.Input)
                    {
                        GeneralTransform targetTransform = item.TransformToVisual(this);
                        Point source = targetTransform.Transform(new Point(0, 0));

                        source.X += item.BoxWidth / 2;
                        source.Y += item.BoxHeight;

                        item.SourcePoint = source;

                        //var t = item.Next.TransformToVisual(this);
                        //var k = t.Transform(new Point(0, 0));
                        //k.X += item.Next.BoxWidth / 2;

                        //item.TargetPoint = k;
                    }
                }

                foreach (var item in v.Output)
                {
                    GeneralTransform targetTransform = item.TransformToVisual(this);
                    Point source = targetTransform.Transform(new Point(0, 0));

                    source.X += item.BoxWidth / 2;
                    source.Y += item.BoxHeight;

                    item.SourcePoint = source;

                    var t = item.Next.TransformToVisual(this);
                    var k = t.Transform(new Point(0, 0));
                    k.X += item.Next.BoxWidth / 2;

                    item.TargetPoint = k;

                    //targetCoords.Offset(item.Next.BoxWidth / 2, 0);

                    //p.Children.Add(new Line() { X1 = targetCoords.X, Y1 = targetCoords.Y, X2 = k.X, Y2 = k.Y, StrokeThickness = 1, Stroke=Brushes.Blue });                    
                    //drawingContext.DrawLine(new Pen(Brushes.Blue, 1), source, k);                                    
                }
            }
            this.InvalidateVisual();
            //(sender as Control).InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            foreach (var v in GraphVertex)
            {
                //if (v is SubgraphControl)
                //{
                //    var s = v as SubgraphControl;
                //    foreach (var item in s.Input)
                //    {
                //        drawingContext.DrawLine(new Pen(Brushes.Blue, 1), item.SourcePoint, item.TargetPoint);
                //    }
                //}
                foreach (var item in v.Output)
                {
                    drawingContext.DrawLine(new Pen(Brushes.Blue, 1), item.SourcePoint, item.TargetPoint);
                }
            }
            //    foreach (var item in v.Output)
            //    {      
            //        if (item.Id.Contains("main::Select"))
            //        {
            //            var a = "a";
            //        }
            //        GeneralTransform targetTransform = item.TransformToVisual(this);
            //        Point source = targetTransform.Transform(new Point(0, 0));

            //        source.X += item.BoxWidth / 2;
            //        source.Y += item.BoxHeight;

            //        var t = item.Next.TransformToVisual(this);
            //        var k = t.Transform(new Point(0, 0));
            //        k.X += item.Next.BoxWidth / 2;
            //        //targetCoords.Offset(item.Next.BoxWidth / 2, 0);

            //        //p.Children.Add(new Line() { X1 = targetCoords.X, Y1 = targetCoords.Y, X2 = k.X, Y2 = k.Y, StrokeThickness = 1, Stroke=Brushes.Blue });

            //        drawingContext.DrawLine(new Pen(Brushes.Blue, 1), source, k);                                    
            //    }
            //}

            //if (v is SubgraphControl)
            //{
            //    var s = v as SubgraphControl;

            //    foreach (var item in s.Input)
            //    {
            //        GeneralTransform targetTransform = item.TransformToVisual(this);
            //        Point targetCoords = targetTransform.Transform(new Point(0, 0));
            //        targetCoords.Offset(item.BoxWidth / 2, item.BoxHeight);


            //        var t = item.Next.TransformToVisual(this);
            //        var k = t.Transform(new Point(0, 0));
            //        k.Offset(item.Next.BoxWidth / 2, 0);

            //        //p.Children.Add(new Line() { X1 = targetCoords.X, Y1 = targetCoords.Y, X2 = k.X, Y2 = k.Y, StrokeThickness = 1, Stroke=Brushes.Blue });

            //        drawingContext.DrawLine(new Pen(Brushes.Blue, 3), targetCoords, k);  
            //    }
            //}



        }

        protected static void Vertex_PropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
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
