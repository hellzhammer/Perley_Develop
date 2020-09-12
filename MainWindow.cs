using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.ComponentModel;
using System;
using System.Web;

using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

using Perley_Develop_Core_lib.App_Components;
namespace Perley_Develop_IDE.GUI
{
    class MainWindow : Window, Perley_Develop_IDE.GUI.IWindow
    {
        private int height = 600;
        private int width = 800;
        [UI] private Box box = null;

        Dictionary<string, TextTag> Tags = new Dictionary<string, TextTag>();

        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);
            
            DeleteEvent += Window_DeleteEvent;
            BuildApp();
            this.Title = this.Title + " -- Directory: " + Session.CurrentSession.ProjectDirectory.Path;
        }
        public void BuildApp(){
            Gtk.Application.Invoke (delegate {
              MainViewBuilder viewBuild = new MainViewBuilder();
              setWindow();  
              box.Spacing = 5;

              MenuBar mb = viewBuild.BuildMenu(this);
              box.Add(mb);
              
              HBox _hbox = viewBuild.BuildTextEditor();
              box.Add(_hbox);
              
              HBox hbox = viewBuild.BuildFooter();
              box.PackEnd(hbox,false, true, 10);

              VPaned pane = viewBuild.BuildConsole();

              this.ShowAll();
            });
        }

        void setWindow(){
            this.HeightRequest = height;
            this.WidthRequest = width;
        }
        
        public void Window_DeleteEvent(object sender, DeleteEventArgs a) => Application.Quit();     
    }
}
