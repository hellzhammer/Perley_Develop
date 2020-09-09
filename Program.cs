using System;
using Gtk;
using System.IO;
using Perley_Develop_IDE.GUI;
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

            var win = new WelcomeWindow();
            app.AddWindow(win);

            win.Show();
            Application.Run();
        }

        private static void FileSystemBuilder(){
            string mainPath = Environment.CurrentDirectory;
            var s = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            Console.WriteLine(s);
            if(!Directory.Exists(s)){
                Directory.CreateDirectory(s+"/PerleyDevProjects");
            }
            else{
                //todo: get paths to projects.
            }
            App_Path.projectPath = s+"/PerleyDevProjects";
        }
    }
}
