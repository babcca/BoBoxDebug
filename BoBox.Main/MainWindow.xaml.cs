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

using BoBox.Deserializer;
using BoBox.Visitors;
using BoBox.Entities;

namespace BoBox.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Graph Model { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            var l = new ModelLoader();
            var m = l.LoadFromFile("Data/q3c.sparql.json");           

            var c = new ModelToControl();
            var p = c.Transfrom(m);

            GraphCanvas.GraphLayers = p;
            
        }
    }
}
