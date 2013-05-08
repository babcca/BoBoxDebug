using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Text;
using System.IO;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using Microsoft.Win32;

namespace BoBox.Deserializer
{    
    public sealed class Config : ConfigData
    {
        private static readonly Config instance = Load();
        
        private Config() {            
        }
        
        public static Config Instance
        {
            get
            {
                return instance;
            }
        }
        
        private static Config Load()
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly();
                        
            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("config.json", FileMode.OpenOrCreate, storage))
            {
                // Nacti jako config
                // Znovu inicializace staticke instance?
                Config instance_ = JsonSerializer.DeserializeFromStream<Config>(stream);          
                return instance_;
            }
        }

        public void Save()
        {
            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly();

            // Uloz jako konfiguracni data (proc ne config?)
            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("config.json", FileMode.OpenOrCreate, storage))
            {                
                JsonSerializer.SerializeToStream<ConfigData>(this, stream);
            }

        }        
    }

    public class ConfigData
    {                
        [Category("Bobolang Settings")]
        [DisplayName("Bobolang compiler path")]
        [Editor(typeof(ExecutableFileEditor), typeof(ITypeEditor))]
        public string BobolangCompiler { get; set; }
        
        [Category("Bobolang Settings")]
        [DisplayName("Bobolang compiler commnad")]
        public string BobolangCommand { get; set; }

        [Category("Bobolang Editor")]
        [DisplayName("Syntax definition path")]
        [Editor(typeof(ExecutableFileEditor), typeof(ITypeEditor))]
        public string SyntaxDefinitionPath { get; set; }


        [Category("BoBox Runtime")]
        [DisplayName("BoBox Runtime commnad")]
        public string BoBoxRuntimeCommand { get; set; }

        [Category("BoBox Runtime")]
        [DisplayName("BoBox runtime path")]
        [Editor(typeof(ExecutableFileEditor), typeof(ITypeEditor))]                
        public string BoBoxRuntime { get; set; }

        

        //[ExpandableObject]        
    }

    public class ExecutableFileEditor : ITypeEditor
    {
        TextBox fileNameInput;

        public System.Windows.FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            DockPanel panel = new DockPanel();
            panel.LastChildFill = true;
            Button browseButton = new Button();
            browseButton.Content = "...";
            browseButton.Click += new RoutedEventHandler(browse_Click);
            DockPanel.SetDock(browseButton, Dock.Right);
            panel.Children.Add(browseButton);

            fileNameInput = new TextBox();
            fileNameInput.Text = "";
            panel.Children.Add(fileNameInput);

            //create the binding from the bound property item to the editor
            var _binding = new Binding("Value"); //bind to the Value property of the PropertyItem
            _binding.Source = propertyItem;
            _binding.ValidatesOnExceptions = true;
            _binding.ValidatesOnDataErrors = true;
            _binding.Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            BindingOperations.SetBinding(fileNameInput, TextBox.TextProperty, _binding);
            return panel;
        }

        private void browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Executable file (*.exe)|*.exe|All Files (*.*)|*.*";
            if (openFile.ShowDialog() == true)
            {
                fileNameInput.Text = openFile.FileName;
                fileNameInput.GetBindingExpression(TextBox.TextProperty).UpdateSource();                
            }
        }
    }
}
