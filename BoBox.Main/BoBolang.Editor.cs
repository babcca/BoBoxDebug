using AvalonDock.Layout;
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

namespace BoBox.Main.Editor
{
    public class BoBolangEditor : LayoutDocument
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

        void BoBolangEditor_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsChanged)
            {
                var status = MessageBox.Show("Ulozit pred zavrenim?", "Ulozit", MessageBoxButton.YesNoCancel);

                switch (status)
                {
                    case MessageBoxResult.Yes:
                        File.WriteAllText(FileName, Document.Text);
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

        public void AtacheToPane(LayoutDocumentPane pane)
        {
            pane.Children.Add(this);
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
}
