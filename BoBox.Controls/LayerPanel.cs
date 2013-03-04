using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace BoBox.Controls
{
    public class LayerPanel : Panel
    {
        // [Todo] Pridat paddingy
        //public double Height { get { return Vertices.Max(v => v.Height); } }
        //public double Width { get { return Vertices.Sum(v => v.Width); } }
                                
        public static readonly DependencyProperty VerticesProperty;

        public List<VertexControl> Vertices
        {
            get { return GetValue(VerticesProperty) as List<VertexControl>; }
            set { SetValue(VerticesProperty, value); }
        }

        static LayerPanel()
        {
            VerticesProperty = DependencyProperty.Register("Vertices", typeof(List<VertexControl>), typeof(LayerPanel), new FrameworkPropertyMetadata(null, Vertices_PropertyChanged));
        }

        protected static void Vertices_PropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var layer = obj as LayerPanel;

            var panel = new StackPanel() { Orientation = Orientation.Horizontal };
            foreach (var item in layer.Vertices)
            {
                panel.Children.Add(item);
            }
            layer.Children.Add(panel);
        }        
        
        //protected override Size MeasureOverride(Size availableSize)
        //{
        //    var size = base.MeasureOverride(availableSize);
        //    return new Size(Width, Height);
        //}
    }
}
