using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BoBox.Controls.Vertices;
using System.Windows.Media;

namespace BoBox.Controls
{
    public class EdgeControl : Control
    {
        public BoBox.Utils.IConsole Console { get; set; }
        static EdgeControl()
        {            
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EdgeControl), new FrameworkPropertyMetadata(typeof(EdgeControl)));
            SourceProperty = DependencyProperty.Register("Source", typeof(DummyControl), typeof(EdgeControl));
            TargetProperty = DependencyProperty.Register("Target", typeof(DummyControl), typeof(EdgeControl));
        }

        public static readonly DependencyProperty SourceProperty;
        public static readonly DependencyProperty TargetProperty;

        public DummyControl Source
        {
            get { return GetValue(SourceProperty) as DummyControl; }
            set { SetValue(SourceProperty,value); }
        }

        public DummyControl Target 
        {
            get { return GetValue(TargetProperty) as DummyControl; }
            set { SetValue(TargetProperty, value); }
        }

        public void RenderLine()
        {
            Panel parent = GetParentPanel(this);
            GeneralTransform sourceTransform = Source.TransformToVisual(parent);
            Point sourceCoords = sourceTransform.Transform(new Point(0, 0));

            GeneralTransform targetTransform = Target.TransformToVisual(parent);
            Point targetCoords = targetTransform.Transform(new Point(0, 0));

            Source.X = sourceCoords.X;            
            Source.Y = sourceCoords.Y;

            TargetX = targetCoords.X;
            TargetY = targetCoords.Y;

            Source.test = sourceCoords.ToString();
            Target.test = targetCoords.ToString();


        }

        private Panel GetParentPanel(FrameworkElement element)
        {
            if (element.Parent == null)
            {
                throw new Exception("No parent panel found");
            }
            else
            {
                var parent = element.Parent as FrameworkElement;
                if (parent is Panel)
                {
                    return parent as Panel;
                }
                else
                {
                    return GetParentPanel(parent);
                }
            }                                   
        }





        public double SourceX
        {
            get { return (double)GetValue(SourceXProperty); }
            set { SetValue(SourceXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SourceX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceXProperty =
            DependencyProperty.Register("SourceX", typeof(double), typeof(EdgeControl));



        public double SourceY
        {
            get { return (double)GetValue(SourceYProperty); }
            set { SetValue(SourceYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SourceY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceYProperty =
            DependencyProperty.Register("SourceY", typeof(double), typeof(EdgeControl));




        public double TargetX
        {
            get { return (double)GetValue(TargetXProperty); }
            set { SetValue(TargetXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TargetX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetXProperty =
            DependencyProperty.Register("TargetX", typeof(double), typeof(EdgeControl));



        public double TargetY
        {
            get { return (double)GetValue(TargetYProperty); }
            set { SetValue(TargetYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TargetY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetYProperty =
            DependencyProperty.Register("TargetY", typeof(double), typeof(EdgeControl));

        
        


        
        
    }
}


