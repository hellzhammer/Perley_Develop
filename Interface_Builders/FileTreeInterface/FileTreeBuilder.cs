using System.IO;
using System.Collections.Generic;
using Gtk;
using System;

using Perley_Develop_Core_lib.App_Components;
namespace Perley_Develop_IDE.Interface_Builders.FileTreeInterface
{
    public class FileTreeBuilder
    {
        public static Dictionary<string, string[]> fileTreeDict = new Dictionary<string, string[]>();
        public static List<string> singleFiles = new List<string>();
        static void GetFiles()
        {
            Dictionary<string, string[]> files = new Dictionary<string, string[]>();
            string[] dirs = Directory.GetDirectories(Session.CurrentSession.projectPath);

            foreach (string dir in dirs)
            {
                if (Directory.Exists(dir))
                {
                    List<string> f = new List<string>();
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        f.Add(file);
                    }
                    files.Add(dir, f.ToArray());
                }
            }

            foreach (var item in Directory.GetFiles(Session.CurrentSession.projectPath))
            {
                singleFiles.Add(item);
            }

            fileTreeDict = files;
        }

        public static TreeView BuildFileTree()
        {
            GetFiles();
            // Create our TreeView
            Gtk.TreeView tree = new Gtk.TreeView();
            tree.WidthRequest = 150;
            // Create a column for the artist name
            Gtk.TreeViewColumn artistColumn = new Gtk.TreeViewColumn();
            artistColumn.Title = "Project";

            // Create the text cell that will display the artist name
            Gtk.CellRendererText artistNameCell = new Gtk.CellRendererText();
            // Add the cell to the column
            artistColumn.PackStart(artistNameCell, true);

            // Add the columns to the TreeView
            tree.AppendColumn(artistColumn);
            //tree.AppendColumn (songColumn);

            // Tell the Cell Renderers which items in the model to display
            artistColumn.AddAttribute(artistNameCell, "text", 0);
            Gtk.TreeStore musicListStore = new Gtk.TreeStore(typeof(string));
            Gtk.TreeIter iter = new TreeIter();
            foreach (var entry in fileTreeDict)
            {
                iter = musicListStore.AppendValues(Path.GetDirectoryName(entry.Key));
                foreach (var e in entry.Value)
                {
                    musicListStore.AppendValues(iter, Path.GetFileName(e));
                }
            }

            foreach (var item in singleFiles)
            {
                musicListStore.AppendValues(Path.GetFileName(item));
            }

            // Assign the model to the TreeView
            tree.Model = musicListStore;

            tree.RowActivated += (sender, args) =>
            {
                Console.WriteLine("Selection made: ");
                Gtk.TreeIter iter;
                musicListStore.GetIter(out iter, args.Path);

                var song = musicListStore.GetValue(iter, 0);
                Console.WriteLine(song.ToString());
            };

            // Show the window and everything on it
            return tree;
        }
    }
}