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
using BoBox.Controls;

namespace BoBox.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Graph Model { get; set; }
        Funq.Container con = new Funq.Container();

        ViewModel.MainViewModel model = new ViewModel.MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            LoadAllNeccessaryThings();

            con.Register<BoBox.Utils.IConsole>((cc) => model.Console);

            con.Register<EdgeControl>((cc) => new EdgeControl() { Console = cc.Resolve<BoBox.Utils.IConsole>() });
            con.Register<Editor.BoBographTab, string>((cc, fileName) => new Editor.BoBographTab(fileName) { Console = cc.Resolve<BoBox.Utils.IConsole>() });
            //var e = con.Resolve<EdgeControl>();
            var l = new ModelLoader();
            var m = l.LoadFromFile("Data/q7.sparql.json");
            new Editor.BoBographTab("Data/q3c.sparql.json").AtacheToPane(MainDocumentsPane);

            //var c = new ModelToControl();
            //var p = c.Transfrom(m);

            //GraphCanvas.GraphLayers = p;
            //BoBox.Editor.TabsManager man = new BoBox.Editor.TabsManager() { Console = model.Console };            
            //Man.Add(new AvalonDock.Layout.LayoutAnchorable() { Title = "AAAA" });
            //Man.Add(new AvalonDock.Layout.LayoutDocument() { Title = "aaa" });
            //Man.Add(new AvalonDock.Layout.LayoutDocument() { Title = "bbb" });
            //Man.Add(new AvalonDock.Layout.LayoutDocument() { Title = "ccc" });
            DataContext = model;
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
                model.o.Add(editor);
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

        private void MenuItem_Graph_Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog browser = new OpenFileDialog();
            browser.ValidateNames = true;
            browser.Filter = "Graph source | *.json";
            //browser.FilterIndex = 1;
            bool? result = browser.ShowDialog();

            if (result.HasValue && result.Value)
            {
                var graph = new Editor.BoBographTab(browser.FileName);
                graph.AtacheToPane(MainDocumentsPane);
            }
        }

        private void MenuItem_Tools_Options(object sender, RoutedEventArgs e)
        {
            var c = BoBox.Deserializer.Config.Instance;
            SettingsWindow w = new SettingsWindow() { SelectedObject = c };
            var a = w.ShowDialog();
            c.Save();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var tab = MainDocumentsPane.GetActiveTab();
            if (tab is Editor.BoBolangEditor)
            {
                e.CanExecute = true;
                e.Handled = true;
            }
            else
            {
                e.CanExecute = false;
                e.Handled = true;
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var tab = MainDocumentsPane.GetActiveTab();
            if (tab is Editor.BoBolangEditor)
            {
                ((Editor.BoBolangEditor)tab).Save();
            }
        }

        private void dockingManager_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Build_Click(object sender, RoutedEventArgs e)
        {
            var compiler = BoBox.Deserializer.Config.Instance.BobolangCompiler;
            var args = BoBox.Deserializer.Config.Instance.BobolangCommand;

            if (!string.IsNullOrWhiteSpace(compiler))
            {
                FileExecution fe = new FileExecution() { Console = model.Console };
                var activeSource = MainDocumentsPane.Children.FirstOrDefault(c => c.IsActive);
                if (activeSource != null)
                {
                    var filename = activeSource.ToolTip.ToString();
                    var cmd = string.Format(args, filename);
                    fe.Run(compiler, cmd);
                }
                else
                {
                    model.Console.Error("No active bobolang file");
                }
            }
            else
            {
                model.Console.Error("BoBolang compiler not set");
            }
        }

        private void RunBoBolang_Click(object sender, RoutedEventArgs e)
        {
            var runtime = BoBox.Deserializer.Config.Instance.BoBoxRuntime;
            var args = BoBox.Deserializer.Config.Instance.BoBoxRuntimeCommand;

            if (!string.IsNullOrWhiteSpace(runtime))
            {
                FileExecution fe = new FileExecution() { Console = model.Console };
                var activeSource = MainDocumentsPane.Children.FirstOrDefault(c => c.IsActive);
                if (activeSource != null)
                {
                    var modelFile = activeSource.ToolTip.ToString();
                    var cmd = string.Format(args, modelFile);
                    fe.Run(runtime, cmd);
                }
                else
                {
                    model.Console.Error("No active bobolang file");
                }
            }
            else
            {
                model.Console.Error("BoBox runtime not set");
            }
        }
    }
}
