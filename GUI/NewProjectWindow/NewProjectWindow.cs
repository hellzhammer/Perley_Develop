using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.ComponentModel;
using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
namespace Perley_Develop_IDE.GUI {
    public class NewProjectWindow : Window ,IWindow {
        [UI] private Box box = null;
        Entry pathEntry { get; set; }
        Entry projEntry { get; set; }
        public NewProjectWindow() : this(new Builder("NewProjectWindow.glade")) { }

        private NewProjectWindow(Builder builder) : base(builder.GetObject("NewProjectWindow").Handle)
        {
            builder.Autoconnect(this);
            DeleteEvent += Window_DeleteEvent;
            this.Resizable = false;
            BuildApp();
        }

        public void BuildApp()
        {
            Gtk.Application.Invoke(delegate
            {
                Label l = new Label("New Project");
                this.box.PackStart(l, false, true, 10);

                HBox projH = new HBox();
                Label l1 = new Label("Project Name: ");
                projEntry = new Entry();
                projEntry.WidthRequest = 100;
                projH.PackStart(l1, false, true, 10);
                projH.Add(projEntry);
                this.box.Add(projH);

                HBox pathH = new HBox();
                Label l2 = new Label("Project Directory: ");
                pathEntry = new Entry();
                pathEntry.Text = App_Path.projectPath;
                pathEntry.WidthRequest = 100;
                pathH.PackStart(l2, false, true, 10);
                pathH.Add(pathEntry);
                Button b = new Button("Browse");
                b.Clicked += BrowseClicked;
                pathH.Add(b);
                this.box.Add(pathH);

                this.ShowAll();
            });
        }
        private void BrowseClicked(object sender, EventArgs e){
            MainViewBuilder b = new MainViewBuilder();
            string s = b.OpenDirectory(this);
            Gtk.Application.Invoke(delegate
            {
                if (s != null) {
                    pathEntry.Text = s;
                }
            });
        }

        public void Window_DeleteEvent(object sender, DeleteEventArgs a) {
            //todo: do something useful on exit
        }
    }
}