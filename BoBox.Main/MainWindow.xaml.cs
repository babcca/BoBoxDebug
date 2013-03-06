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
using Microsoft.Win32;
using System.IO;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

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
            LoadAllNeccessaryThings();
            Funq.Container con = new Funq.Container();


            var l = new ModelLoader();
            var m = l.LoadFromFile("Data/q7.sparql.json");

            var c = new ModelToControl();
            var p = c.Transfrom(m);

            GraphCanvas.GraphLayers = p;

        }

        private void MenuItem_BoBox_Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog browser = new OpenFileDialog();
            browser.ValidateNames = true;            
            browser.Filter = "BoBolang source | *.bobolang;*.txt";
            //browser.FilterIndex = 1;
            bool? result = browser.ShowDialog();

            if (result.HasValue && result.Value)
            {
                var editor = new Editor.BoBolangEditor(browser.FileName);
                editor.AtacheToPane(MainDocumentsPane);
            }
        }

        private void MenuItem_BoBox_New(object sender, RoutedEventArgs e)
        {            
            SaveFileDialog browser = new SaveFileDialog();
            browser.Filter = "BoBolang source | *.bobolang";
            browser.AddExtension = true;                        
            browser.OverwritePrompt = true;
            browser.ValidateNames = true;
            
            bool? result = browser.ShowDialog();

            if (result.HasValue && result.Value)
            {
                var editor = new Editor.BoBolangEditor(browser.FileName);
                editor.AtacheToPane(MainDocumentsPane);
            }            
            
        }

        private void LoadAllNeccessaryThings()
        {
            var syntaxFiles = Directory.EnumerateFiles("./Syntax/", "*.xshd", SearchOption.TopDirectoryOnly);

            foreach (var syntaxFile in syntaxFiles)
            {
                string name = System.IO.Path.GetFileNameWithoutExtension(syntaxFile);
                string extension = string.Format(".{0}", name.ToLower());
                
                HighlightingManager.Instance.RegisterHighlighting(name, new string[] { extension }, () => LoadHighlighting(syntaxFile));
            }
        }

        private IHighlightingDefinition LoadHighlighting(string fileName)
        {
            using (var syntax = File.OpenRead(fileName))
            {
                using (XmlTextReader reader = new XmlTextReader(syntax))
                {
                    var highlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
                    return highlighting;
                }
            }
        }        
    }
}
