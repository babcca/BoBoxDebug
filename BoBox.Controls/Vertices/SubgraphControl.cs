using BoBox.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BoBox.Controls.Vertices
{
    public class SubgraphControl : VertexControl
    {
        static SubgraphControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SubgraphControl), new FrameworkPropertyMetadata(typeof(SubgraphControl)));
            LabelProperty.AddOwner(typeof(SubgraphControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

            VerticesProperty = DependencyProperty.Register("Vertices", typeof(List<VertexControl>), typeof(SubgraphControl));
            //GraphProperty = DependencyProperty.Register("Graph", typeof(Graph), typeof(SubgraphControl));
        }

        public SubgraphControl()
            : base()
        {

        }

        public SubgraphControl(Subgraph vertex)
            : base(vertex)
        {
        }

        public static readonly DependencyProperty VerticesProperty;
        //public static readonly DependencyProperty GraphProperty;

        public List<VertexControl> Vertices
        {
            get { return GetValue(VerticesProperty) as List<VertexControl>; }
            set { SetValue(VerticesProperty, value); }
        }        
    }
}
