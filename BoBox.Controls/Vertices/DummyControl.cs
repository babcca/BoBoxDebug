using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BoBox.Controls.Vertices
{
    public class DummyControl : Control
    {
        static DummyControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DummyControl), new FrameworkPropertyMetadata(typeof(DummyControl)));                       
        }

        public DummyControl()
        {            
        }

        public Panel Parent { get; set; }

        protected override void OnRender(System.Windows.Media.DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            
        }

        protected override void ParentLayoutInvalidated(UIElement child)
        {
            base.ParentLayoutInvalidated(child);
            //var p = GetParentPanel(this);
            //GeneralTransform targetTransform = this.TransformToVisual(p);
            //Point targetCoords = targetTransform.Transform(new Point(0, 0));

        }

        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        // Using a DependencyProperty as the backing store for X.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(DummyControl));



        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Y.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(DummyControl));



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

        public string test
        {
            get { return (string)GetValue(testProperty); }
            set { SetValue(testProperty, value); }
        }

        // Using a DependencyProperty as the backing store for test.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty testProperty =
            DependencyProperty.Register("test", typeof(string), typeof(DummyControl), new FrameworkPropertyMetadata("aaaa"));
                
        
    }
}
