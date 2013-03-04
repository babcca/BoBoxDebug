using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BoBox.Controls.Vertices
{
    public class DummyControl : Control
    {
        static DummyControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DummyControl), new FrameworkPropertyMetadata(typeof(DummyControl)));
            XProperty = DependencyProperty.Register("X", typeof(int), typeof(DummyControl));
            YProperty = DependencyProperty.Register("Y", typeof(int), typeof(DummyControl));
        }

        public int X { get; set; }
        public int Y { get; set; }

        public static readonly DependencyProperty XProperty;
        public static readonly DependencyProperty YProperty;
    }
}
