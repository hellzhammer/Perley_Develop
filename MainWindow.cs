using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.ComponentModel;
using System;
using System.Web;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Perley_Develop_IDE
{
    class MainWindow : Window
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
        }

        private TreeView BuildFileTree(){
            Gtk.TreeView tree = new Gtk.TreeView ();
            Gtk.TreeViewColumn artistColumn = new Gtk.TreeViewColumn ();
            artistColumn.Title = "Artist";
            Gtk.TreeViewColumn songColumn = new Gtk.TreeViewColumn ();
            songColumn.Title = "Song Title";
            tree.AppendColumn (artistColumn);
            tree.AppendColumn (songColumn);
            Gtk.ListStore musicListStore = new Gtk.ListStore (typeof (string), typeof (string));
            tree.Model = musicListStore;
            return tree;
        }

        private void BuildApp(){
            Gtk.Application.Invoke (delegate {
              MainViewBuilder viewBuild = new MainViewBuilder();
              setWindow();  
              box.Spacing = 5;

              MenuBar mb = viewBuild.BuildMenu();
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

        
        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }
    }
}
