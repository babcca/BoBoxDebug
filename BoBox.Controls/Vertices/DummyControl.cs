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
            MouseEnter += DummyControl_MouseEnter;
            MouseLeave += DummyControl_MouseLeave;
        }

        void DummyControl_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var originalColor = Brushes.White;
            
            var next = Next;

            if (next != null)
            {
                Background = originalColor;
            }

            while (next != null)
            {
                next.Background = originalColor;
                next = next.Next;
            }
        }

        void DummyControl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {            
            var activeColor = Brushes.Green;
            
            var next = Next;
            if (next != null)
            {
                Background = activeColor;
            }
            while (next != null)
            {
                next.Background = activeColor;
                next = next.Next;
            }
        }

        public DummyControl(string id, string pathId)
            :this()
        {
            Id = id;
            PathId = pathId;            
        }

        public string Id { get;  set; }
        public string PathId { get; set; }

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






        public double BoxWidth
        {
            get { return ActualWidth; }
            set { SetValue(WidthProperty, value); }
        }

        

        public double BoxHeight
        {
            get { return ActualHeight; }
            set { SetValue(HeightProperty, value); }
        }

        
        

        
        

        


        public Point SourcePoint
        {
            get { return (Point)GetValue(SourcePointProperty); }
            set { SetValue(SourcePointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SourcePoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourcePointProperty =
            DependencyProperty.Register("SourcePoint", typeof(Point), typeof(DummyControl));



        public Point TargetPoint
        {
            get { return (Point)GetValue(TargetPointProperty); }
            set { SetValue(TargetPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TargetPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetPointProperty =
            DependencyProperty.Register("TargetPoint", typeof(Point), typeof(DummyControl));

        




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




        public DummyControl Next
        {
            get { return (DummyControl)GetValue(NextProperty); }
            set { SetValue(NextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Next.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NextProperty =
            DependencyProperty.Register("Next", typeof(DummyControl), typeof(DummyControl));                
    }
}
