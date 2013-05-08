using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using global::AvalonDock;
using global::AvalonDock.Layout;

namespace BoBox.Graphics
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class AvalonDockManager : UserControl
    {
        
        public List<LayoutDocument> DocumentList { get; set; }
        public List<LayoutAnchorable> AnchorableList { get; set; }
        

        public AvalonDockManager()
        {
            InitializeComponent();
            DocumentList = new List<LayoutDocument>();
            AnchorableList = new List<LayoutAnchorable>();
        }       
    }
}
