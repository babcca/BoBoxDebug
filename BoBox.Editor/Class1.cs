using AvalonDock.Layout;
using BoBox.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace BoBox.Editor
{

    public interface IToolbarRegister
    {
        List<ToolbarButton> GetToolbarItem();
    }
    
    public class ToolbarButton
    {
        public string Title { get; set; }
        public string IcoPath { get; set; }
        public ICommand Command { get; set; }
    }

    


    public class TabsManager : LayoutDocumentPane
    {
        public IConsole Console { get; set; }

        private ObservableCollection<BaseTab> tabList = new ObservableCollection<BaseTab>();

        private BaseTab lastActiveTab = null;

        public TabsManager()
        {
            
        }

        public void AddTab(BaseTab tab)
        {
            tabList.Add(tab);
            tab.OnAddHandler(this);
            
            //tab.Closed += tab_Closed;
            //tab.IsActiveChanged += tab_IsActiveChanged;
            //tab.IsSelectedChanged += tab_IsSelectedChanged;
        }

        void tab_IsSelectedChanged(object sender, EventArgs e)
        {
            Console.Log("Selected change");            
        }

        void tab_IsActiveChanged(object sender, EventArgs e)
        {
            Console.Log("Active change");
        }

        

        public void RemoveTab(BaseTab tab)
        {
            tabList.Remove(tab);
        }

        void tab_Closed(object sender, EventArgs e)
        {
            RemoveTab((BaseTab)sender);
        }        
    }


    public class BaseTab : LayoutDocument
    {        
        public delegate void OnAdd(TabsManager manager);

        public OnAdd OnAddHandler { get; protected set; }

        public BaseTab()
        {
            
        }
    }
}
