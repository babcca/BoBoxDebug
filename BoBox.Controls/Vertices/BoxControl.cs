using BoBox.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BoBox.Controls.Vertices
{
    public class BoxControl : VertexControl
    {
        public static readonly DependencyProperty InputProperty;
        public ObservableCollection<DummyControl> Input 
        { 
            get { return GetValue(InputProperty) as ObservableCollection<DummyControl>; }
            set { SetValue(InputProperty, value); }
        }

        public static readonly DependencyProperty OutputProperty;
        public ObservableCollection<DummyControl> Output
        {
            get { return GetValue(OutputProperty) as ObservableCollection<DummyControl>; }
            set { SetValue(OutputProperty, value); }
        }

        static BoxControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BoxControl), new FrameworkPropertyMetadata(typeof(BoxControl)));
            LabelProperty.AddOwner(typeof(BoxControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

            InputProperty = DependencyProperty.Register("Input", typeof(ObservableCollection<DummyControl>), typeof(BoxControl));
            OutputProperty = DependencyProperty.Register("Output", typeof(ObservableCollection<DummyControl>), typeof(BoxControl));
        }

        public BoxControl()
            : base()
        {
            init();
        }

        public BoxControl(Box vertex)
            : base(vertex)
        {
            init();
        }

        private void init()
        {
            Input = new ObservableCollection<DummyControl>();
            Input.Add(new DummyControl());
            Input.Add(new DummyControl());
            Input.Add(new DummyControl());
            Input.Add(new DummyControl());
            Input.Add(new DummyControl());

            Output = new ObservableCollection<DummyControl>();
            Output.Add(new DummyControl());
            Output.Add(new DummyControl());
            
        }

    }
}
