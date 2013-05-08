using AvalonDock.Layout;
using BoBox.Deserializer;
using BoBox.Visitors;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BoBox.Main.Editor
{

    public class TabManager : LayoutDocumentPane
    {
        public TabManager()
        {       
                 
        }

        public BaseTab GetActiveTab()
        {
            return this.Children.FirstOrDefault(c => c.IsActive == true) as BaseTab;
        }

        public string ActiveTab { get { return this.SelectedContent.Title; } }
    }

    public class BaseTab : LayoutDocument
    {
        public bool IsAtached { get; set; }

        public BaseTab()
        {
            IsActiveChanged += BaseTab_IsActiveChanged;
        }

        void BaseTab_IsActiveChanged(object sender, EventArgs e)
        {
            var a = sender as BaseTab;
            // a.accept(toolbarVisitor)            
        }

        public void AtacheToPane(LayoutDocumentPane pane)
        {
            pane.Children.Add(this);
        }

        protected void TouchFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Close();
            }
        }
        protected string GetDocumentTitle(string fileName)
        {
            return Path.GetFileName(fileName);
        }

        protected string GetDocumentTooltip(string fileName)
        {
            return Path.GetFullPath(fileName);
        }

    }

    public class BoBographTab : BaseTab
    {
        public BoBox.Utils.IConsole Console { get; set; }

        public BoBographTab(string sourceFile)
        {
            var s = System.Diagnostics.Stopwatch.StartNew();
            var l = new ModelLoader();
            var m = l.LoadFromFile(sourceFile);
            var c = new ModelToLayeredControl();
            var p = c.Transfrom(m);
            s.Stop();
            var e = s.ElapsedTicks;
            var ms = s.ElapsedMilliseconds;
            System.Diagnostics.Debug.WriteLine("{0} {1}", e, ms);
            
            var model = new BoBox.Controls.GraphCanvasControl();
            model.GraphLayers = p;            

            var scroll = new ScrollViewer() { HorizontalScrollBarVisibility = ScrollBarVisibility.Visible };
            scroll.Content = model;
            Content = scroll;

            Title = GetDocumentTitle(sourceFile);
            ToolTip = GetDocumentTooltip(sourceFile);

            IsSelected = true;
        }
    }

    public class BoBolangEditor : BaseTab
    {
        public bool IsChanged { get; private set; }
        public string FileName { get; private set; }
        public TextDocument Document { get; private set; }

        public BoBolangEditor(string sourceFile)
        {
            TouchFile(sourceFile);
            Content = GetTextEditor(sourceFile);
            Title = GetDocumentTitle(sourceFile);
            ToolTip = GetDocumentTooltip(sourceFile);
            Closing += BoBolangEditor_Closing;            
            IsSelected = true;

            IsChanged = false;
            FileName = sourceFile;
        }

        public void Save()
        {
            if (IsChanged)
            {
                File.WriteAllText(FileName, Document.Text);
                IsChanged = false;
                Title = string.Format("{0}", GetDocumentTitle(FileName));
            }
        }

        void BoBolangEditor_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsChanged)
            {
                var status = MessageBox.Show("Ulozit pred zavrenim?", "Ulozit", MessageBoxButton.YesNoCancel);

                switch (status)
                {
                    case MessageBoxResult.Yes:
                        Save();
                        e.Cancel = false;
                        break;
                    case MessageBoxResult.No:
                        e.Cancel = false;
                        break;
                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
                        break;
                    default:
                        throw new Exception(string.Format("Spatna odpoved {0}", status));
                }
            }
            else
            {
                e.Cancel = false;
            }
        }


        protected TextEditor GetTextEditor(string sourceFile)
        {
            Document = new TextDocument();
            Document.Text = File.ReadAllText(sourceFile);

            TextEditor editor = new TextEditor();
            editor.Document = Document;
            editor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(Path.GetExtension(sourceFile));
            editor.TextChanged += TextChanged_Event;
            return editor;
        }

        private void TextChanged_Event(object sender, EventArgs e)
        {
            if (!IsChanged)
            {
                IsChanged = true;
                Title = string.Format("* {0}", Title);
            }
        }       
    }
}
