using AvalonDock.Layout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace BoBox.LayoutControl
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public List<LayoutDocument> DocumentList { get; set; }
        public List<LayoutAnchorable> AnchorableList { get; set; }

        public LayoutDocument LastActiveDocument { get; set; }

        public UserControl1()
        {
            InitializeComponent();
            DocumentList = new List<LayoutDocument>();
            AnchorableList = new List<LayoutAnchorable>();                        
        }

        public void Add(LayoutDocument document)
        {
            DocumentList.Add(document);
            DocumentPane.Children.Add(document);
            document.IsActiveChanged += document_IsActiveChanged;            
        }
        
        void document_IsActiveChanged(object sender, EventArgs e)
        {
            var doc = sender as LayoutDocument;
            if (doc.IsActive)
            {
                LastActiveDocument = doc;
            }
        }

        public void Add(LayoutAnchorable anchorable)
        {
            AnchorableList.Add(anchorable);
            AnchorablePane.Children.Add(anchorable);
        }

        private void DockingManager_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
