using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Collections.ObjectModel;
using BoBox.Controls.Vertices;

namespace BoBox.Controls
{
    public abstract class VertexControl : Control
    {

               

        static VertexControl()
        {
            LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(VertexControl));
            XProperty = DependencyProperty.Register("X", typeof(double), typeof(VertexControl));
            YProperty = DependencyProperty.Register("Y", typeof(double), typeof(VertexControl));

            InputProperty = DependencyProperty.Register("Input", typeof(ObservableCollection<DummyControl>), typeof(VertexControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));
            OutputProperty = DependencyProperty.Register("Output", typeof(ObservableCollection<DummyControl>), typeof(VertexControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));
        }

        public VertexControl()
            : base()
        {
            Input = new ObservableCollection<DummyControl>();
            Output = new ObservableCollection<DummyControl>();            
        }
        
        #region Dependency properties        
        public static readonly DependencyProperty LabelProperty;
        public static readonly DependencyProperty XProperty;
        public static readonly DependencyProperty YProperty;

        public static readonly DependencyProperty OutputProperty;
        public static readonly DependencyProperty InputProperty;
        #endregion                      
        
        //protected override void OnRender(DrawingContext drawingContext)
        //{
        //    base.OnRender(drawingContext);
        //    foreach (var item in Output)
        //    {
        //        // draw line betwean Objects
        //        //item;
        //        //item.Next;

        //        //var p = GetParentPanel(this);
        //        //item.tra
        //        //GeneralTransform targetTransform =  item.TransformToVisual(p);
        //        //Point targetCoords = targetTransform.Transform(new Point(0, 0));
                
        //        //var t = item.Next.TransformToVisual(p);
        //        //var k = t.Transform(new Point(0, 0));

        //        ////p.Children.Add(new Line() { X1 = targetCoords.X, Y1 = targetCoords.Y, X2 = k.X, Y2 = k.Y, StrokeThickness = 1, Stroke=Brushes.Blue });

        //        //drawingContext.DrawLine(new Pen(Brushes.Blue, 1), targetCoords, k);

        //    }
        //}

        //private Panel GetParentPanel(FrameworkElement element)
        //{
        //    if (element.Parent == null)
        //    {
        //        throw new Exception("No parent panel found");
        //    }
        //    else
        //    {
        //        var parent = element.Parent as FrameworkElement;
        //        if (parent is GraphCanvasControl)
        //        {
        //            return parent as Panel;
        //        }
        //        else
        //        {
        //            return GetParentPanel(parent);
        //        }
        //    }
        //}

        #region Properties
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public double X
        { 
            get { return (double) GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public double Y
        {
            get { return (double) GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }
        
        public ObservableCollection<DummyControl> Input
        {
            get { return GetValue(InputProperty) as ObservableCollection<DummyControl>; }
            set { SetValue(InputProperty, value); }
        }

        
        public ObservableCollection<DummyControl> Output
        {
            get { return GetValue(OutputProperty) as ObservableCollection<DummyControl>; }
            set { SetValue(OutputProperty, value); }
        }

        #endregion
    }
}
