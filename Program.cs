using System.Linq;
using System;
using Gtk;
using System.IO;
using Perley_Develop_IDE.GUI;
using Perley_Develop_IDE.Perley_Dev_System.Plugin;
namespace Perley_Develop_IDE
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            /*
            //testing out loading plugins
            PerleyDevPluginLoader loader = new PerleyDevPluginLoader();
            loader.LoadPlugins(args);
            */
            IDE_Startup();
            Application.Init();

            var app = new Application("org.Perley_Develop_IDE.Perley_Develop_IDE", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            var win = new WelcomeWindow();
            app.AddWindow(win);

            win.Show();
            Application.Run();
        }

        private static void IDE_Startup(){
            //build the projects folder if not done already
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

            //Load editor plugin paths
            Console.WriteLine("Getting Editor Components..");
            //get the contents of the plugins Core folder
            App_Path.CorePlugins = Directory.GetDirectories(App_Path.GetCorePath()).ToList();
            //get the contents of the plugins CoreGui folder
            App_Path.CoreGuiPlugins = Directory.GetDirectories(App_Path.GetGuiPath()).ToList();
            Console.WriteLine("Finished Getting Editor Components..");
        }
    }
}
