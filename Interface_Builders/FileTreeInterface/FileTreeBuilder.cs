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

        /*
        Need to implement some new logic in here.
        this breaks the app currently but i have a new implementation 
        i have added in an external library.

        after implementing that i need to rework the below tree builder to iterate through 
        everyhting and display the content as expected.
        */

        public static TreeView BuildFileTree()
        {
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