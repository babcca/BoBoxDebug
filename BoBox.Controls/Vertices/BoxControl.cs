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
        static BoxControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BoxControl), new FrameworkPropertyMetadata(typeof(BoxControl)));
            LabelProperty.AddOwner(typeof(BoxControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));


        }

        public BoxControl()
            : base()
        {
        }
    }
}
