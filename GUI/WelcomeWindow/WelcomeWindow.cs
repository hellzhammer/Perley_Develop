using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.ComponentModel;
using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Perley_Develop_IDE.GUI
{
    public class WelcomeWindow : Window, Perley_Develop_IDE.GUI.IWindow
    {
        [UI] private Box box = null;
        public WelcomeWindow() : this(new Builder("WelcomeWindow.glade")) { }

        private WelcomeWindow(Builder builder) : base(builder.GetObject("WelcomeWindow").Handle)
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
                Label l = new Label("Welcome To Perley Develop");
                this.box.PackStart(l, true, false, 10);
                Button newButton = new Button("New Project");
                MainViewBuilder build = new MainViewBuilder();
                newButton.Clicked += (sender, args) => {
                    //todo
                    var win = new NewProjectWindow();
                    Application.AddWindow(win);
                    win.Show();
                };
                Button openButton = new Button("Open Project");
                openButton.Clicked += (sender, args) =>
                {
                    string dir = build.OpenDirectory(this);
                    if(dir != null){
                        string[] dirs = DirectoryManager.GetDirectorySubdirectories(dir);
                        foreach (var folder in dirs)
                        {
                            Console.WriteLine(folder);
                        }
                    }
                };

                this.box.Add(newButton);
                this.box.Add(openButton);
                this.ShowAll();
            });
        }
        public void Window_DeleteEvent(object sender, DeleteEventArgs a) => Application.Quit();
    }
}