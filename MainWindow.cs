using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.ComponentModel;
using System;
using System.Web;

using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

using Perley_Develop_Core_lib.App_Components;
using Perley_Develop_IDE.Global;
namespace Perley_Develop_IDE.GUI
{
    class MainWindow : Window, Perley_Develop_IDE.GUI.IWindow
    {
        private int height = 600;
        private int width = 800;
        [UI] private Box box = null;

        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);
            SystemComponents.IDE_Console = new ConsoleWidget(ConsoleType.IDE);
            DeleteEvent += Window_DeleteEvent;
            BuildApp();
            this.Title = this.Title + " -- Directory: " + Session.CurrentSession.ProjectDirectory.Path;
            SystemComponents.IDE_Console.DisplayConsoleMessage("IDE prep complete", "IDE is ready to use", DateTime.Now.ToLocalTime());
        }
        public void BuildApp()
        {
            Gtk.Application.Invoke(delegate
            {
                //setup
                MainViewBuilder viewBuild = new MainViewBuilder();
                setWindow();
                box.Spacing = 5;

                //menu bar
                MenuBar mb = viewBuild.BuildMenu(this);
                box.Add(mb);

                //footer
                HBox hbox = viewBuild.BuildFooter();
                //console
                VBox vv = new VBox();
                vv.PackStart(new Label("Output"), false, true, 5);
                ScrolledWindow scrollw = new ScrolledWindow();
                scrollw.Add(SystemComponents.IDE_Console);
                vv.Add(scrollw);
                //text editor
                HPaned _hbox = viewBuild.BuildTextEditor();
                VPaned vvv = new VPaned();
                vvv.Pack1(_hbox, false, true);
                vvv.Pack2(vv, false, true);

                box.PackEnd(hbox, false, true, 10);
                box.PackEnd(vvv, false, true, 10);
                this.ShowAll();
            });
        }

        void setWindow()
        {
            this.HeightRequest = height;
            this.WidthRequest = width;
        }

        public void Window_DeleteEvent(object sender, DeleteEventArgs a) => Application.Quit();
    }
}
