using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BoBox.Controls.Vertices;

namespace BoBox.Controls
{
    public class EdgeControl : Control
    {
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
    }
}

