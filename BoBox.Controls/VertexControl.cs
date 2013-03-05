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

using BoBox.Entities.Interfaces;
using BoBox.Entities;
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

            InputProperty = DependencyProperty.Register("Input", typeof(ObservableCollection<DummyControl>), typeof(VertexControl));
            OutputProperty = DependencyProperty.Register("Output", typeof(ObservableCollection<DummyControl>), typeof(VertexControl));
        }

        public VertexControl()
            : base()
        {
        }

        public VertexControl(IVertex vertex)
            : this()
        {
            Vertex = vertex;
            Label = vertex.Label;

            Input = new ObservableCollection<DummyControl>();
            foreach (var item in vertex.Inputs)
            {
                Input.Add(new DummyControl());
            }

            Output = new ObservableCollection<DummyControl>();
            foreach (var item in vertex.Outputs)
            {
                Output.Add(new DummyControl());
            }

        }

        #region IVertex decorator
        public IVertex Vertex { get; private set; }

        public string Id
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public List<DummyVertex> Inputs
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public List<DummyVertex> Outputs
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Type
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        
        public void Accept(IVertexVisitor visitor)
        {
            throw new NotImplementedException();
        }
        public TResult Accept<TResult>(IVertexVisitor<TResult> visitor)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Dependency properties        
        public static readonly DependencyProperty LabelProperty;
        public static readonly DependencyProperty XProperty;
        public static readonly DependencyProperty YProperty;

        public static readonly DependencyProperty OutputProperty;
        public static readonly DependencyProperty InputProperty;
        #endregion                      

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
