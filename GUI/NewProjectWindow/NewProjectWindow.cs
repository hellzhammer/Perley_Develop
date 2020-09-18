using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.ComponentModel;
using System;

using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

using Perley_Develop_Core_lib.App_Components;
using Perley_Develop_Core_lib.Dotnet;

namespace Perley_Develop_IDE.GUI {
    public class NewProjectWindow : Window ,IWindow {
        [UI] private Box box = null;
        Entry pathEntry { get; set; }
        Entry projEntry { get; set; }
        string[] appTypes = new string[]{"gtkapp", "console", "classlib", "webapi", "webapp"};
        int selectedAppType = 0;
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

                string newPath = App_Path.projectPath + "/";

                pathEntry.Text = newPath;
                pathEntry.WidthRequest = 100;
                pathH.PackStart(l2, false, true, 10);
                pathH.Add(pathEntry);
                Button b = new Button("Browse");
                b.Clicked += BrowseClicked;
                pathH.Add(b);
                this.box.Add(pathH);

                var dropdown = new ComboBox(appTypes);
                dropdown.Changed += (sender, args)=>{
                    Console.WriteLine(dropdown.Active);
                    selectedAppType = dropdown.Active;
                };
                this.box.Add(dropdown);

                HBox buttons = new HBox();
                Button cancel = new Button("Cancel");
                cancel.Clicked += CancelClicked;
                Button proceed = new Button("Continue"); 
                proceed.Clicked += ContinueClicked;
                buttons.Add(proceed);
                buttons.Add(cancel);
                this.box.Add(buttons);

                this.ShowAll();
            });
        }
        private void CancelClicked(object sender, EventArgs e){
            if(Session.CurrentSession != null)
            {
                Session.CurrentSession.EndSession(false);
            }
            var win = new WelcomeWindow();
            Application.AddWindow(win);
            win.Show();
            this.Dispose();
        }
        private void ContinueClicked(object sender, EventArgs e){
            if(string.IsNullOrEmpty(pathEntry.Text)){
                //todo: show missing text dialogue...
                MessageDialog md = new MessageDialog (this, 
                DialogFlags.DestroyWithParent, MessageType.Error, 
                ButtonsType.Close, "Missing Directory Path!");
                md.Run();
                md.Dispose();
                return;
            }
            if(string.IsNullOrEmpty(projEntry.Text)){
                //todo: show missing text dialogue...
                MessageDialog md = new MessageDialog (this, 
                DialogFlags.DestroyWithParent, MessageType.Error, 
                ButtonsType.Close, "Missing Project Name!");
                md.Run();
                md.Dispose();
                return;
            }
            string pathToUse = pathEntry.Text + projEntry.Text;
            Directory.CreateDirectory(pathToUse);
            DotnetCommander dComm = new DotnetCommander();
            dComm.CreateApp(appTypes[selectedAppType], pathToUse);
            Session sesh = new Session(pathToUse);
            var win = new MainWindow();
            Application.AddWindow(win);
            win.Show();
            this.Dispose();
        }
        private void BrowseClicked(object sender, EventArgs e){
            MainViewBuilder b = new MainViewBuilder();
            string s = b.OpenDirectory(this);

            if (s != null) {
                Gtk.Application.Invoke(delegate
                {
                    pathEntry.Text = s;
                });                    
                Session.CurrentSession.UpdateProjectDir(s);
            }
        }

        public void Window_DeleteEvent(object sender, DeleteEventArgs a) {
            //todo: do something useful on exit
        }
    }
}