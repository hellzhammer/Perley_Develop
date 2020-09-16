using System.Linq;
using System;
using Gtk;
using System.IO;
using Perley_Develop_IDE.GUI;
using Perley_Develop_Core_lib.FileSystem.IDE;
namespace Perley_Develop_IDE
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            IDE_Startup.IDEStartup();

            Application.Init();
            var app = new Application("org.Perley_Develop_IDE.Perley_Develop_IDE", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);
            var win = new WelcomeWindow();
            app.AddWindow(win);
            win.Show();
            Application.Run();
        }
    }
}
