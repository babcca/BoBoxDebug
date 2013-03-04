using BoBox.Entities;
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
    
    public class SubgraphControl : VertexControl
    {
        private bool firstExpand_ = true;
        public bool FirstExpand { get { return firstExpand_; } }

        public static readonly DependencyProperty GraphLayersProperty;
        public StackPanel GraphLayers
        {
            get { return GetValue(GraphLayersProperty) as StackPanel; }
            set { SetValue(GraphLayersProperty, value); }
        }


        static SubgraphControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SubgraphControl), new FrameworkPropertyMetadata(typeof(SubgraphControl)));
            LabelProperty.AddOwner(typeof(SubgraphControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

            VerticesProperty = DependencyProperty.Register("Vertices", typeof(List<VertexControl>), typeof(SubgraphControl), new FrameworkPropertyMetadata(null, Vertices_PropertyChanged));
            IsExpandProperty = DependencyProperty.Register("IsExpand", typeof(bool), typeof(SubgraphControl), new FrameworkPropertyMetadata(false, IsExpanded_PropertyChanged));

            GraphLayersProperty = DependencyProperty.Register("GraphLayers", typeof(StackPanel), typeof(SubgraphControl), new FrameworkPropertyMetadata(null, a_PropertyChanged));
        }

        protected static void a_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {                        
        }

        private static void Vertices_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var b = "";
        }

        protected static void IsExpanded_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var subgraph = d as SubgraphControl;
            if (subgraph.IsExpand && subgraph.FirstExpand)
            {                
            }

        }

        public SubgraphControl()
            : base()
        {

        }

        public SubgraphControl(Subgraph vertex)
            : base(vertex)
        {
        }

        public delegate void OnFirstExpandHandler(List<VertexControl> vertices);

        public OnFirstExpandHandler OnFirstExpand { get; set; }

        public static readonly DependencyProperty VerticesProperty;
        public static readonly DependencyProperty IsExpandProperty;
        //public static readonly DependencyProperty GraphProperty;

        public List<VertexControl> Vertices
        {
            get { return GetValue(VerticesProperty) as List<VertexControl>; }
            set { SetValue(VerticesProperty, value); }
        }

        public bool IsExpand
        {
            get { return (bool)GetValue(IsExpandProperty); }
            set { SetValue(IsExpandProperty, value); }
        }
    }
}
