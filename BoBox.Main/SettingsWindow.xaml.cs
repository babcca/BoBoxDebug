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
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.PropertyGrid;

using System.Windows.Shapes;
using System.ComponentModel;


namespace BoBox.Main
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public object SelectedObject 
        { 
            get { return grid.SelectedObject; }
            set { grid.SelectedObject = value; } 
        }

        public SettingsWindow()
        {
            InitializeComponent();
        }        
    }   
}
