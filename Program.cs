using System;
using Gtk;
using System.IO;

namespace Perley_Develop_IDE
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            FileSystemBuilder();

            Application.Init();

            var app = new Application("org.Perley_Develop_IDE.Perley_Develop_IDE", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            var win = new MainWindow();
            app.AddWindow(win);

            win.Show();
            Application.Run();
        }

        private static void FileSystemBuilder(){
            if(!Directory.Exists(Environment.CurrentDirectory+"/Projects")){
                Directory.CreateDirectory(Environment.CurrentDirectory+"/Projects");
            }
        }
    }
}
